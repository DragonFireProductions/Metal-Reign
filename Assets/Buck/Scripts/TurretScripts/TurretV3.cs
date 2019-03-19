//-------------------------------------------------------------------------------------------------------------------------
//Purpose: This script defines how the turret acts depending on its ammunition type. This handles all effects
//That go along with the corresponding type of ammo.
//
//
//-------------------------------------------------------------------------------------------------------------------------
using UnityEngine;

[System.Serializable]
public class TurretV3 : MonoBehaviour
{

    [Header("Use Bullets")]
    [HideInInspector]
    //This is this turrets ammo prefab
    public GameObject bulletPrefab;
    [HideInInspector]
    //The particle prefab for this turret
    public GameObject muzzleBlast;
    [HideInInspector]
    public AudioClip bulletFire;

    [Header("Use Explosives")]
    [HideInInspector]
    public GameObject explosivePrefab;

    [HideInInspector]
    public AudioClip explosiveFire;

    [Header("Use Laser")]
    [HideInInspector]
    public LineRenderer laser;

    [HideInInspector]
    public ParticleSystem laserImpactEffect;

    //NOT IN USE AT THE MOMENT
    [HideInInspector]
    public Light[] laserImpactLights;

    //MOVE THIS OVER TO THE PROJECTILE MANAGER WHEN YOU FIGURE IT OUT
    [HideInInspector]
    public float damageOverTime;

    [Header("General")]
    [HideInInspector]
    public GameObject turretRangeObj;
    [HideInInspector]
    public bool useLaser = false;
    [HideInInspector]
    public bool useExplosive = false;
    [HideInInspector]
    public bool useBullet = false;
    //the range of this turret
    [HideInInspector]
    public float range;
    //How fast this turret rotates at
    [HideInInspector]
    public float rotateSpeed;
    //The enemy that this turret will fire at
    [SerializeField]
    string enemytag = "Enemy";
    //How often/fast this turret fires at
    [HideInInspector]
    public float fireRate;

    [Header("Turret Setup")]
    [HideInInspector]
    public AudioSource turretAudioSource;
    //Empty game object placed in front of the barrel
    [HideInInspector]
    public Transform turretFirePoint;
    //Empty game object that rotates
    //The head and barrel
    [HideInInspector]
    public Transform partToRotate;
    //Target is considered the first enemy that enters this turrets range
    [HideInInspector]
    public Transform turretTarget;
    //Keeps track of the time between shots fired from this turret
    float fireTime;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTurretTarget", 0f, 0.5f);

        turretAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //The * 2 is a quick fix to getting the range obj the same size as the actual range
        turretRangeObj.GetComponent<Transform>().localScale = new Vector3(range * 2, range * 2, range * 2);

        if (turretTarget != null)
        {
            ObstructionCheck();
        }
        else
        {
            return;
        }
    }

    void UseProjectile()
    {
        fireTime += Time.deltaTime;

        // time to fire
        if (fireTime > fireRate)
        {
            //Create projectile instance
            GameObject projectileGO = Instantiate(bulletPrefab, turretFirePoint.position, turretFirePoint.rotation);

            //Get the script from the bullet GameObject
            TurretProjectileManagerV3 projectile = projectileGO.GetComponent<TurretProjectileManagerV3>();

            if (projectile != null)
            {
                projectile.Seek(turretTarget);
            }
            else
            {
                return;
            }

            turretAudioSource.clip = bulletFire;

            turretAudioSource.Play();

            //Create particle effect at turretFirePoint
            GameObject newMuzzleBlast = Instantiate(muzzleBlast, turretFirePoint.position, turretFirePoint.rotation);

            //Destroy the particle effect based on the turrets fireRate
            Destroy(newMuzzleBlast, fireRate);

            // reset fire time
            fireTime = 0.0f;
        }
    }

    public void UseLaser()
    {
        //This is only a temp fix, figure your shit out Mike, move this over to the projectile when your not total suk
        turretTarget.GetComponent<EnemyV2>().TakeDamage(damageOverTime * Time.deltaTime);

        if (!laser.enabled)
        {
            laser.enabled = true;
            laserImpactEffect.Play();
        }
        //Sets the beginning of the laser to be at the firepoint
        laser.SetPosition(0, turretFirePoint.position);
        //Sets the end position to be on the enemies position
        laser.SetPosition(1, turretTarget.position);

        //stores the fire points position minue the targets position
        Vector3 dir = turretFirePoint.position - turretTarget.position;

        //sets the laserimpact to the targets position
        laserImpactEffect.transform.position = turretTarget.position + dir.normalized;

        //This sets the transform to look in the direction of dir
        //Which was previously set
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void UseExplosive()
    {
        fireTime += Time.deltaTime;

        // time to fire
        if (fireTime > fireRate)
        {
            //Create projectile instance
            GameObject projectileGO = Instantiate(explosivePrefab, turretFirePoint.position, turretFirePoint.rotation);

            //Get the script from the bullet GameObject
            TurretProjectileManagerV3 projectile = projectileGO.GetComponent<TurretProjectileManagerV3>();

            if (projectile != null)
            {
                projectile.Seek(turretTarget);
            }
            else
            {
                return;
            }

            turretAudioSource.clip = explosiveFire;

            turretAudioSource.Play();

            //Create particle effect at turretFirePoint
            GameObject newMuzzleBlast = Instantiate(muzzleBlast, turretFirePoint.position, turretFirePoint.rotation);

            //Destroy the particle effect based on the turrets fireRate
            Destroy(newMuzzleBlast, fireRate);

            // reset fire time
            fireTime = 0.0f;
        }
    }

    void UpdateTurretTarget()
    {
        //Find the tag named enemy tag of all the objects within the
        //Array of GameObjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            turretTarget = nearestEnemy.transform;
        }
        else
        {
            turretTarget = null;
        }
    }

    void LockOnTarget()
    {

        //Turn from current transform, to the targets transform
        Vector3 dir = turretTarget.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);

        //This is so that if an enemy exits the turrets range and another comes in
        //It doesn't snap right to the next target
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

        //The pivot point turns on the y rotation only 
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //Checks to see if there are walls or environment obstructing the turrets view of the target
    void ObstructionCheck()
    {
        RaycastHit hit;

        if (Physics.Linecast(turretFirePoint.position, turretTarget.position, out hit) && hit.transform == turretTarget)
        {
            LockOnTarget();

            if (turretTarget == null)
            {
                if (useLaser)
                {
                    if (laser.enabled)
                    {
                        laser.enabled = false;
                        laserImpactEffect.Stop();
                    }
                }
                return;
            }

            if (useLaser)
            {
                UseLaser();
            }

            if (useBullet)
            {
                UseProjectile();
            }

            if (useExplosive)
            {
                UseExplosive();
            }
        }

        //LOOK INTO THIS LOGIC, NEED TO MAKE SURE THE TURRET IS CHECKING FOR WALLS, AND ENVIRONMENT FOR FUTURE
        else if (Physics.Linecast(turretFirePoint.position, turretTarget.position, out hit) && hit.transform != turretTarget)
        {
            if (hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "Environment")
            {
                Debug.DrawRay(turretFirePoint.position, transform.TransformDirection(turretFirePoint.forward) * hit.distance, Color.red);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //This needs to be fixed so that the new turret range object properly matches the range of the turret
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
