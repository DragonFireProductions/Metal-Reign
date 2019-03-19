using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyV2 : MonoBehaviour
{
    [Header("EnemyStats")]

    //How far the enemy can detect the player from
    [SerializeField]
    float detectRange;

    //How fast this enemy moves
    [SerializeField]
    float moveSpeed;

    //this is this enemies max health
    [SerializeField]
    float maxHealth;

    //How much health this enemy has currently
    float curHealth;

    //How much this enemy is worth once killed
    public int worth;

    //The particle effect this enemy gives off when health reaches 0
    [SerializeField]
    GameObject deathEffect;

    [Header("UnitySettings")]

    [SerializeField]
    Image healthBar;

    [SerializeField]
    string turretTag = "Turret";

    [SerializeField]
    string playerTag = "Player";

    //Transform target;

    //int wavePointIndex = 0;

    //This will need to be found upon instantiation to make sure it is populated correctly
    public Transform target;

    public Transform player;

    public Transform turret;

    NavMeshAgent agent;

    // Use this for initialization
    void Start ()
    {
        //Search the scene for the game object tagged as "EnemyDestination"
        target = GameObject.FindGameObjectWithTag("EnemyDestination").transform;

        //This sets the current health of this enemy 
        //to the maximum alloted health
        curHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(target.transform.position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Navigation();

        //NOT WORKING AT THE MOMENT
        //PlayerCheck();

        TurretCheck();
    }

    void Navigation()
    {

        RaycastHit hit;

        agent.SetDestination(target.transform.position);

        agent.speed = moveSpeed;

        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            DestinationReached();
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        }
    }

    //THIS DOESN'T WORK AT THE MOMENT I NEED TO FIGURE THIS OUT A BIT LATER
    void PlayerCheck()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null && shortestDistance <= detectRange)
        {
            player = nearestPlayer.transform;
        }
        else
        {
            player = null;
        }
    }

    void TurretCheck()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag(turretTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestTurret = null;

        foreach (GameObject turret in turrets)
        {
            float distanceToTurret = Vector3.Distance(transform.position, turret.transform.position);

            if (distanceToTurret < shortestDistance)
            {
                shortestDistance = distanceToTurret;
                nearestTurret = turret;
            }
        }

        if (nearestTurret != null && shortestDistance <= detectRange)
        {
            turret = nearestTurret.transform;
        }
        else
        {
            turret = null;
        }
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;

        //sets the health to alway start at 1 and end at 0
        healthBar.fillAmount = curHealth / maxHealth;

        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject, 1.5f);
    }

    void DestinationReached()
    {
        PlayerStats.lives--;
        Destroy(gameObject);

        if (PlayerStats.lives <= 0)
        {
            PlayerStats.lives = 0;
            return;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
