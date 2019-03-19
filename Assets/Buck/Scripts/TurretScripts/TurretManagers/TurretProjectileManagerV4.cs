using UnityEngine;

public class TurretProjectileManagerV4 : MonoBehaviour
{
    [Header("Bullet Settings")]

    //[HideInInspector]
    public GameObject bulletImpactEffect;
    //[HideInInspector]
    //This is this turrets ammo prefab
    public GameObject bulletPrefab;
    //[HideInInspector]
    public AudioClip bulletFire;

    [Header("General")]
    //Figure out a way to hide this when useLaser is true
    //[HideInInspector]
    public float projectileSpeed;
    //How much damage the bullets inflict on the enemies
    //this float is meant to be referenced by enemy classes
    //Figure out a way to hide this when useLaser is true
    //[HideInInspector]
    public float damage;

    //This is the enemy that the bullet will seek
    //[HideInInspector]
    public Transform bulletTarget;

    public void Seek(Transform target)
    {
        bulletTarget = target;
    }

    public void FireBullet()
    {
        if (bulletTarget == null)
        {
            Destroy(bulletPrefab);
            return;
        }

        //Looks in the direciton of the target
        Vector3 dir = bulletTarget.position - transform.position;

        //This is a quick math process determining the distance traveled in one frame
        float distanceTraveled = projectileSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceTraveled)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distanceTraveled, Space.World);
    }

    //This is referenced within each of the bullet functions above
    public void TargetHit()
    {
        GameObject effectClone = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectClone, 1f);
        DamageEnemy(bulletTarget);
        Destroy(bulletPrefab);
    }

    //This will be used to damage the enemy that the bullet hits
    public void DamageEnemy(Transform enemy)
    {
        EnemyV2 e = enemy.GetComponent<EnemyV2>();

        //If the enemy has the Enemy script
        //Do the stuff
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
