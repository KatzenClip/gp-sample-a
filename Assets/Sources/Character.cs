using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Projectile projectile_Origin = null;

    [SerializeField] List<Projectile> listProjectile = null;

    void Update()
    {
        UpdateInput();
        UpdateCheckDestroyProjectile();
    }

    void UpdateLookAtTarget()
    {
        if (null == target) return;

        transform.LookAt(target);
    }

    void FireProjectileToTarget()
    {
        var projectile = Instantiate(projectile_Origin);
        if (null == projectile) return;

        AimToTarget(projectile);

        projectile.transform.position = transform.position;
        projectile.SetTarget(target);

        listProjectile.Add(projectile);
    }

    void AimToTarget(Projectile _projectile)
    {
        _projectile.transform.LookAt(target);
        _projectile.transform.Rotate(-60f, 0, 0);
    }

    void UpdateCheckDestroyProjectile()
    {
        for (int n = listProjectile.Count - 1, zero = 0; zero <= n; --n)
        {
            var go = listProjectile[n];
            if (go.IsHit)
            {
                Destroy(go.gameObject);
                listProjectile.Remove(go);
                break;
            }
        }
    }

    void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpdateLookAtTarget();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectileToTarget();
        }
    }
}
