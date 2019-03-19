//--------------------------------------------------------------
//Purpose: This is a central class that all turret ammo types
//Will call from, from here all that is needed is to 
//Create a turret ammo type script and feed through the 
//Specific function that you need to the child class that
//Inherits from this class. This specifically deals with
//dealing damage/effects to enemies, as well as handels the
//Instantiation of the ammo models
//
//By: Michael Buck II
//--------------------------------------------------------------
using UnityEngine;

public class TurretProjectileManagerV3 : MonoBehaviour
{
    [Header("Bullet Settings")]

    [HideInInspector]
    public GameObject bulletImpactEffect;

    [Header("Explosive Settings")]
    //This is how large of an explosion the bullet makes
    //Upon impact 
    [HideInInspector]
    public float explosionRadius;

    [HideInInspector]
    public ParticleSystem smokeTrail;

    //This is only here to be cleaned up with its smoke trail
    [HideInInspector]
    public GameObject missile;

    [HideInInspector]
    public GameObject explosionEffect;

    [HideInInspector]
    public float waitTime;

    [Header("Laser Settings")]
    //NOT IN USE YET
    [HideInInspector]
    public float damageOverTime;

    //NOT IN USE YET
    [HideInInspector]
    public LineRenderer laser;

    //NOT IN USE YET
    [HideInInspector]
    public ParticleSystem laserImpactEffect;

    //NOT IN USE AT THE MOMENT
    [HideInInspector]
    public Light[] laserImpactLights;

    [Header("General")]
    //Figure out a way to hide this when useLaser is true
    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public bool useLaser = false;
    [HideInInspector]
    public bool useExplosive = false;
    [HideInInspector]
    public bool useBullet = false;
    //How much damage the bullets inflict on the enemies
    //this float is meant to be referenced by enemy classes
    //Figure out a way to hide this when useLaser is true
    [HideInInspector]
    public float damage;

    //This is the enemy that the bullet will seek
    [HideInInspector]
    public Transform bulletTarget;

    [HideInInspector]
    public Transform turretFP;

    float waitTimer;

    bool didDamage = false;

    public void Seek(Transform target)
    {
        bulletTarget = target;
    }

    public void FireBullet()
    {
        if (bulletTarget == null)
        {
            Destroy(gameObject);
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

    public void FireExplosive()
    {
        if (bulletTarget == null)
        {
            WaitForSmoke();
            return;
        }

        Vector3 dir = bulletTarget.position - transform.position;

        float distanceTraveled = projectileSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceTraveled)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distanceTraveled, Space.World);

        //Look at the enemy so that the "tip of the rocket
        //Always faces forward
        transform.LookAt(bulletTarget);
    }

    //I have spent too much time trying to get this imbeded within this script instead of the turret script itself
    //For now I am moving on to other tasks
    //Michael J. Buck 4/6/18
    public void FireLaser()
    {
        //turretFP = GetComponent<TurretV3>().turretFirePoint;

        //if (!laser.enabled)
        //{
        //    laser.enabled = true;
        //    laserImpactEffect.Play();
        //    TargetHit();
        //}
        ////Sets the beginning of the laser to be at the firepoint
        //laser.SetPosition(0, turretFirePoint.position);
        ////Sets the end position to be on the enemies position
        //laser.SetPosition(1, bulletTarget.position);

        ////stores the fire points position minue the targets position
        //Vector3 dir = turretFirePoint.position - bulletTarget.position;

        ////sets the laserimpact to the targets position
        //laserImpactEffect.transform.position = bulletTarget.position + dir.normalized;

        ////This sets the transform to look in the direction of dir
        ////Which was previously set
        //laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    //This is referenced within each of the bullet functions above
    public void TargetHit()
    {
        if (useBullet)
        {
            GameObject effectClone = Instantiate(bulletImpactEffect, transform.position, transform.rotation);

            Destroy(effectClone, 1f);
        }

        if (useExplosive)
        {
            if (!didDamage)
            {
                GameObject effectClone = Instantiate(explosionEffect, transform.position, transform.rotation);

                Destroy(effectClone, 1f);
            }
        }

        if (useLaser)
        {
            //I am not sure what I will do with this just yet
        }

        if (explosionRadius > 0f)
        {
            Explode();
        }

        else
        {
            DamageEnemy(bulletTarget);
            Destroy(gameObject);
        }
    }

    //This handles the logic for ammo that is considered explosive
    //I.E. mortar shells or rockets
    public void Explode()
    {
        if (!didDamage)
        {
            Collider[] objectsHit = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider collider in objectsHit)
            {
                if (collider.tag == "Enemy")
                {
                    WaitForSmoke();
                    if (!didDamage)
                    {
                        DamageEnemy(collider.transform);
                    }
                }
            }

            didDamage = true;
        }
    }

    public void WaitForSmoke()
    {
        waitTimer += Time.deltaTime;

        //Destroy just the missile
        Destroy(missile);

        //Stop the smoke trail
        smokeTrail.Stop();

        if (waitTimer >= waitTime)
        {
            didDamage = false;
            Destroy(gameObject);
        }
    }

    void OnDrawGizomsSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    //This will be used to damage the enemy that the bullet hits
    public void DamageEnemy(Transform enemy)
    {
        EnemyV2 e = enemy.GetComponent<EnemyV2>();

        //If the enemy has the Enemy script
        //Do the stuff
        if (e != null)
        {
            if (useLaser)
            {
                e.TakeDamage(damageOverTime * Time.deltaTime);
            }
            //Passes through the bullet damage to the take damage function within
            //The enemy script
            e.TakeDamage(damage);
        }
    }
}
