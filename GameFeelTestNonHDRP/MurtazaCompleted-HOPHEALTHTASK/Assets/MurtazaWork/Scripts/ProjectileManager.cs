using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    
    public static ProjectileManager Instance { get => instance; }
    [SerializeField]
    private ProjectileDataWrapper projectileDataWrapper;

    private void Awake()
    {
        instance = this;
    }

    public ProjectileData GetProjectileData(ProjectileCategory projectileCategory)
    {
        ProjectileData projectileData = null;
        switch (projectileCategory)
        {
            case ProjectileCategory.Small:
                projectileData = projectileDataWrapper.projectilePool[0];
                break;
            case ProjectileCategory.Medium:
                projectileData = projectileDataWrapper.projectilePool[1];
                break;
            case ProjectileCategory.Mega:
                projectileData = projectileDataWrapper.projectilePool[2];
                break;
            default:
                projectileData = projectileDataWrapper.projectilePool[0];
                break;
        }
        return projectileData;
    }


}
