using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    // buy menu flags
    static bool buy_turret_0 = false;
    static bool buy_turret_1 = false;
    static bool buy_turret_2 = false;

    //Mouse locked check for toggle
    public bool mouseLocked = false;

    [Header("Chassis")]
    public GameObject chassis;

    public Transform[] weaponAttachmentPts;

    public float chassisRotationSpeed;

    public float chassisHP;

    public float chassisArmor;

    //This is housed under the chassis because the chassis is what moves the entire player
    public float movementSpeed;

    [Header("Cockpit")]
    public GameObject cockpitPivotPoint;

    public Transform cockpitPivotPointRotation;

    public GameObject cockpit;

    public float cockpitRotationSpeed;

    public float cockpitHP;

    public float cockpitArmor;

    //This is used for combining stats that both the chassis and cockpit have
    [Header("Player")]

    float playerHP;

    float playerArmor;

    //[Header("Weapons")]
    //[SerializeField]
    //GameObject[] weapons;
    //[SerializeField]
    //GameObject[] weaponPivotPoints;
    //[SerializeField]
    //GameObject[] weaponFirePoints;
    //[SerializeField]
    //float weaponRotationSpeed;
    //[SerializeField]
    //float maxRotationUp;
    //[SerializeField]
    //float maxRotationDown;

    [Header("Turrets")]
    [SerializeField]
    GameObject turret1;

    [SerializeField]
    GameObject turret2;

    [SerializeField]
    GameObject turret3;

    [Header("Ground")]
    [SerializeField]
    GameObject ground;

    void Start ()
    {
        mouseLocked = true;

        if (mouseLocked == true)
        {
            // lock cursor to center of screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
	
	void Update ()
    {
        //Handles the movements of the players different components
        ChassisMovement();
        CockpitMovement();
        WeaponMovement();

        //Other functions that have to deal with the player
        MouseUnlock();
        DrawTurretMesh();
        CalculatePlayerStats();
	}

    void ChassisMovement()
    {
        //Rotate Left/Right
        float moveY = Input.GetAxisRaw("Horizontal");

        //Move Forward/Backward
        float moveZ = Input.GetAxisRaw("Vertical");

        Transform chassisTransform = chassis.GetComponent<Transform>();

        if (moveY != 0)
        {
            chassisTransform.Rotate(0.0f, moveY * chassisRotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }

        if (moveZ != 0)
        {
            chassisTransform.Translate(0.0f, 0.0f, moveZ * movementSpeed * Time.deltaTime, Space.Self);
        }
    }

    void CockpitMovement()
    {
        //Moves that cockpit left/right
        float mouseX = Input.GetAxisRaw("Mouse X");

        Transform cockpitTransform = cockpit.GetComponent<Transform>();

        if (mouseX != 0)
        {
            cockpitTransform.Rotate(0.0f, mouseX * cockpitRotationSpeed * Time.deltaTime, 0.0f, Space.World);
            cockpitTransform.LookAt(cockpitTransform.forward + cockpitTransform.position);
        }
    }

    void WeaponMovement()
    {

    }

    void CalculatePlayerStats()
    {
        playerHP = chassisHP + cockpitHP;

        playerArmor = chassisArmor + cockpitArmor;
    }

    void MouseUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mouseLocked == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLocked = false;
                return;
            }

            if (mouseLocked == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLocked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (mouseLocked == true)
            {
                buy_turret_0 = true;
                buy_turret_1 = false;
                buy_turret_2 = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLocked = false;
                return;
            }

            if (mouseLocked == false)
            {
                buy_turret_0 = false;
                buy_turret_1 = false;
                buy_turret_2 = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLocked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (mouseLocked == true)
            {
                buy_turret_0 = false;
                buy_turret_1 = true;
                buy_turret_2 = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLocked = false;
                return;
            }

            if (mouseLocked == false)
            {
                buy_turret_0 = false;
                buy_turret_1 = false;
                buy_turret_2 = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLocked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (mouseLocked == true)
            {
                buy_turret_0 = false;
                buy_turret_1 = false;
                buy_turret_2 = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLocked = false;
                return;
            }

            if (mouseLocked == false)
            {
                buy_turret_0 = false;
                buy_turret_1 = false;
                buy_turret_2 = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLocked = true;
                return;
            }
        }
    }

    void DrawTurretMesh()
    {
        if (buy_turret_0)
        {
            Camera camera = chassis.GetComponentInChildren<Camera>();

            Transform turret_transform = turret1.GetComponent<Transform>();
            Transform ground_transform = ground.GetComponent<Transform>();
            Vector3 turret_position = turret_transform.position;

            Plane plane = new Plane(Vector3.up, ground_transform.position);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            float distance = 0.0f;
            if (plane.Raycast(ray, out distance))
            {
                turret_transform.position = ray.GetPoint(distance);

                GameObject turretPrefab1 = Instantiate(turret1, turret_transform.position + turret_position + Vector3.up, turret_transform.rotation);

                if (Input.GetMouseButtonUp(1))
                {
                    // reset states
                    Cursor.lockState = CursorLockMode.Locked;
                    buy_turret_0 = false;

                    return;
                }

                Destroy(turretPrefab1, Time.deltaTime * 0.95f);
            }
        }

        if (buy_turret_1)
        {
            Camera camera = chassis.GetComponentInChildren<Camera>();

            Transform turret_transform = turret1.GetComponent<Transform>();
            Transform ground_transform = ground.GetComponent<Transform>();
            Vector3 turret_position = turret_transform.position;

            Plane plane = new Plane(Vector3.up, ground_transform.position);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            float distance = 0.0f;
            if (plane.Raycast(ray, out distance))
            {
                turret_transform.position = ray.GetPoint(distance);

                GameObject turretPrefab2 = Instantiate(turret2, turret_transform.position + turret_position + Vector3.up, turret_transform.rotation);

                if (Input.GetMouseButtonUp(2))
                {
                    // reset states
                    Cursor.lockState = CursorLockMode.Locked;
                    buy_turret_1 = false;

                    return;
                }

                Destroy(turretPrefab2, Time.deltaTime * 0.95f);
            }
        }

        if (buy_turret_2)
        {
            Camera camera = chassis.GetComponentInChildren<Camera>();

            Transform turret_transform = turret1.GetComponent<Transform>();
            Transform ground_transform = ground.GetComponent<Transform>();
            Vector3 turret_position = turret_transform.position;

            Plane plane = new Plane(Vector3.up, ground_transform.position);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            float distance = 0.0f;
            if (plane.Raycast(ray, out distance))
            {
                turret_transform.position = ray.GetPoint(distance);

                GameObject turretPrefab3 = Instantiate(turret3, turret_transform.position + turret_position + Vector3.up, turret_transform.rotation);

                if (Input.GetMouseButtonUp(3))
                {
                    // reset states
                    Cursor.lockState = CursorLockMode.Locked;
                    buy_turret_2 = false;

                    return;
                }

                Destroy(turretPrefab3, Time.deltaTime * 0.95f);
            }
        }
    }
}
