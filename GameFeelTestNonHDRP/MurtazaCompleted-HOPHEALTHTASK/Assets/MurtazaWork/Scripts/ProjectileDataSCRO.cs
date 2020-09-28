using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ProjectileCategory
{
    Small,
    Medium,
    Mega
}

[System.Serializable]
public class ProjectileData
{
    public float speed = 10;
    public ParticleSystem dmgEffect;
    public float waitTime = 2f;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;
    public float particleSizeFactor = 0.5f;
    public bool applyForce;
    public float explosionForce = 10f;
}
[System.Serializable]
public class ProjectileDataWrapper
{
    [SerializeField]
    public List<ProjectileData> projectilePool;

    public ProjectileDataWrapper()
    {
        this.projectilePool = new List<ProjectileData>();
    }
}