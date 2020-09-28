using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileCategory typeOfProjectile = ProjectileCategory.Small;

    [SerializeField]
    private Transform targetObj;

    private float distFromTarget = 0f;
    private float targetImpactETA = 0f;
    private float timePassed = 0f;

    private bool targetHit = false;
    private Vector3 dmgSystemPos;
    private StressReceiver stressReceiver;
    private Rigidbody targetObjRB;
    private ProjectileData projectileData;
    private float waitTime = 0f;
    private void Start()
    {
        projectileData = ProjectileManager.Instance.GetProjectileData(typeOfProjectile);
        distFromTarget = (targetObj.position - transform.position).magnitude;
        targetImpactETA = distFromTarget / projectileData.speed;
        stressReceiver = Camera.main.GetComponent<StressReceiver>();
        targetObjRB = targetObj.GetComponent<Rigidbody>();
        waitTime = projectileData.waitTime;
    }

    void Update()
    {
        if (targetHit || projectileData == null)
            return;

        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            if (timePassed < targetImpactETA)
            {
                transform.Translate(transform.forward * projectileData.speed * Time.deltaTime);
                timePassed += Time.deltaTime;
            }
            else
            {
                targetHit = true;
                DisableChilds();
                GenerateVFX();
            }

        }
    }

    private void GenerateVFX()
    {
        ParticleSystem dmgsystem = Instantiate<ParticleSystem>(projectileData.dmgEffect, Vector3.zero, Quaternion.identity);
        dmgSystemPos = targetObj.position;
        dmgSystemPos.y += targetObj.localScale.y * 0.5f;
        dmgsystem.transform.position = dmgSystemPos;
        dmgsystem.transform.localScale = Vector3.one * projectileData.particleSizeFactor;
        dmgsystem.Play();
        float distance = Vector3.Distance(stressReceiver.transform.position, dmgSystemPos);
        float distance01 = Mathf.Clamp01(distance / projectileData.Range);
        float stress = (1 - Mathf.Pow(distance01, 2)) * projectileData.MaximumStress;
        stressReceiver.InduceStress(stress);
        if (projectileData.applyForce)
        {
            targetObjRB.AddForce(-targetObj.forward * (projectileData.explosionForce * projectileData.particleSizeFactor), ForceMode.Impulse);
        }

    }

    private void DisableChilds()
    {
        List<Transform> childrens = transform.GetComponentsInChildren<Transform>().ToList();
        childrens.RemoveAt(0);
        childrens.ForEach(x => x.gameObject.SetActive(false));
    }

}
