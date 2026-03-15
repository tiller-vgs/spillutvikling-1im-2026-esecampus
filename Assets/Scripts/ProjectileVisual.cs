using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Projectile projectile;

    private void Update()
    {
        UpdateProjectileRotation();

    }
    private void UpdateProjectileRotation() {
        Vector3 projectileMoveDir = projectile.GetProjectileMoveDir();

        //projectileVisual.transform.rotation = Quaternion.Euler(0, 0, Mathf, Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg);
    }
}
