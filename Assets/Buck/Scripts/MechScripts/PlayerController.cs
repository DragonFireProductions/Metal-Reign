using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // static variables
    static float fire_time    = 0.0f;
    static bool  zoomed_out   = true;

    // buy menu flags
    static bool  buy_turret_0 = false;
    static bool  buy_turret_1 = false;
    static bool  buy_turret_2 = false;

    [Header("Tread")]
    [SerializeField]
    GameObject tread;
    [SerializeField]
    float treadRotationSpeed;
    [SerializeField]
    float treadTranslationSpeed;

    [Header("Chassis")]
    [SerializeField]
    GameObject chassisPivotPoint;
    [SerializeField]
    GameObject chassis;
    [SerializeField]
    float chasisRotationSpeed;

    [Header("Barrel")]
    [SerializeField]
    GameObject barrelPivotPoint;
    [SerializeField]
    float barrelRotationSpeed;
    [SerializeField]
    float barrelMaxDegreesUp;
    [SerializeField]
    float barrelMaxDegreesDown;

    [Header("Fire")]
    [SerializeField]
    GameObject firePoint;
    [SerializeField]
    float fireRate;
    //[SerializeField]
    //PlayerBullet bullet;

    [Header("Camera")]
    [SerializeField]
    GameObject zoomIn;
    [SerializeField]
    GameObject zoomOut;
    [SerializeField]
    float cameraTranslationSpeed;

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

	void Start () {
        // lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }	
	void Update () {
        ZoomInstant();
        ProcessTreadMovement();
        ProcessChassisMovement();
        ProcessWeaponMovement();
        ProcessWeaponFire();

        // testing drawing of the mesh
        MouseUnlock();
        DrawTurretMesh();
    }

    void ZoomInstant()
    {
        bool mouse_middle_button = Input.GetMouseButtonDown(2);

        if (mouse_middle_button)
        {
            Camera camera = chassis.GetComponentInChildren<Camera>();

            if (zoomed_out)
            {
                // retrieve transforms
                Transform camera_transform = camera.transform;
                Transform zoom_in_transform = zoomIn.transform;

                // retrieve scale
                Vector3 camera_scale = camera_transform.localScale;

                // retrieve position
                Vector3 zoom_in_position = zoom_in_transform.position;
                Vector3 zoom_in_euler_angles = zoom_in_transform.eulerAngles;

                // move camera to the fire point position
                camera_transform.localScale = camera_scale * 5.0f;
                camera_transform.position = zoom_in_position;
                camera_transform.eulerAngles = zoom_in_euler_angles;
            }
            else
            {
                // retrieve transforms
                Transform camera_transform = camera.transform;
                Transform zoom_out_transform = zoomOut.transform;

                // retrieve scale
                Vector3 camera_scale = camera_transform.localScale;

                // retrieve position
                Vector3 zoom_out_position = zoom_out_transform.position;

                // move camera to the storage position
                camera.transform.localScale = camera_scale * 0.2f;
                camera.transform.position = zoom_out_position;
            }

            zoomed_out = !zoomed_out;
        }
    }
    void ProcessTreadMovement() {
        // retrieve keyboard/joystick input
        float delta_x = Input.GetAxisRaw("Horizontal");
        float delta_y = Input.GetAxisRaw("Vertical");

        // retrieve the transforms
        Transform chassis_transform = chassis.GetComponent<Transform>();
        Transform chassis_pivot_transform = chassisPivotPoint.GetComponent<Transform>();
        Transform tread_transform = tread.GetComponent<Transform>();

        // left/right movement
        if (delta_x != 0)
        {
            tread_transform.Rotate(0.0f, delta_x * treadRotationSpeed * Time.deltaTime, 0.0f, Space.World);
            chassis_transform.position = chassis_pivot_transform.position;
        }
        // forward/backward movement
        if (delta_y != 0)
        {
            tread_transform.Translate(0.0f, 0.0f, delta_y * treadTranslationSpeed * Time.deltaTime, Space.Self);
            chassis_transform.position = chassis_pivot_transform.position;
        }
    }
    void ProcessChassisMovement() {
        // retrieve mouse input
        float mouse_delta_x = Input.GetAxisRaw("Mouse X");

        //rotation on x axis
        if (mouse_delta_x != 0)
        {
            // retrieve the chassis transform
            Transform chassis_transform = chassis.GetComponent<Transform>();

            chassis_transform.Rotate(0.0f, mouse_delta_x * chasisRotationSpeed * Time.deltaTime, 0.0f, Space.World);
            chassis_transform.LookAt(chassis_transform.forward + chassis_transform.position);
        }
    }
    void ProcessWeaponMovement()
    {
        float mouse_delta_y = Input.GetAxisRaw("Mouse Y");

        // rotation on y axis
        if (mouse_delta_y != 0)
        {

            Transform barrel_transform = barrelPivotPoint.transform;

            barrel_transform.Rotate(mouse_delta_y * -barrelRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
            barrel_transform.LookAt(barrel_transform.forward + barrel_transform.position);

            Vector3 barrel_euler_angles = barrel_transform.eulerAngles;
            if (barrel_euler_angles.x > 180.0f)
            {
                barrel_euler_angles.x -= 360.0f;
            }

            if (barrel_euler_angles.x > barrelMaxDegreesDown)
            {
                barrel_euler_angles.x = barrelMaxDegreesDown;
                barrel_transform.eulerAngles = barrel_euler_angles;
            }
            else if (barrel_euler_angles.x < -barrelMaxDegreesUp)
            {
                barrel_euler_angles.x = -barrelMaxDegreesUp;
                barrel_transform.eulerAngles = barrel_euler_angles;
            }

            if (!zoomed_out)
            {
                Camera camera = chassis.GetComponentInChildren<Camera>();

                Transform camera_transform = camera.GetComponent<Transform>();
                Transform zoom_in_transform = zoomIn.GetComponent<Transform>();

                camera_transform.position = zoom_in_transform.position;

                camera_transform.Rotate(mouse_delta_y * -barrelRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
                camera_transform.LookAt(camera_transform.forward + camera_transform.position);

                Vector3 camera_euler_angles = camera_transform.eulerAngles;
                if (camera_euler_angles.x > 180.0f)
                {
                    camera_euler_angles.x -= 360.0f;
                }

                if (camera_euler_angles.x > barrelMaxDegreesDown)
                {
                    camera_euler_angles.x = barrelMaxDegreesDown;
                    camera_transform.eulerAngles = camera_euler_angles;
                }
                else if (camera_euler_angles.x < -barrelMaxDegreesUp)
                {
                    camera_euler_angles.x = -barrelMaxDegreesUp;
                    camera_transform.eulerAngles = camera_euler_angles;
                }
            }
        }
    }
    void ProcessWeaponFire()
    {
        // retrieve mouse input
        bool left_mouse_button = Input.GetMouseButton(0);

        if (left_mouse_button)
        {
            fire_time += Time.deltaTime;

            // time to fire
            if (fire_time > fireRate)
            {
                Transform fire_point_transform = firePoint.transform;
                Vector3 fire_point_position = fire_point_transform.position;
                Quaternion fire_point_rotation = fire_point_transform.rotation;

                // create bullet instance
                //Instantiate(bullet, fire_point_position, fire_point_rotation);
                // reset fire time
                fire_time = 0.0f;
            }
            return;
        }

        // reset fire time
        fire_time = 0.0f;
    }

    // test functions
    //Don't hate me for the spaghetti code pls
    //I have a back up in a text file on my PC just ask for it
    //EZ revert
    void MouseUnlock()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buy_turret_0 = true;
            buy_turret_1 = false;
            buy_turret_2 = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buy_turret_0 = false;
            buy_turret_1 = true;
            buy_turret_2 = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buy_turret_0 = false;
            buy_turret_1 = false;
            buy_turret_2 = true;
            Cursor.lockState = CursorLockMode.None;
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

                GameObject turretPrefab1 = Instantiate(turret1, turret_transform.position + turret_position +Vector3.up, turret_transform.rotation);

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

                if (Input.GetMouseButtonUp(1))
                {
                    // reset states
                    Cursor.lockState = CursorLockMode.Locked;
                    buy_turret_0 = false;

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

                if (Input.GetMouseButtonUp(1))
                {
                    // reset states
                    Cursor.lockState = CursorLockMode.Locked;
                    buy_turret_0 = false;

                    return;
                }

                Destroy(turretPrefab3, Time.deltaTime * 0.95f);
            }
        }
    }

    // not in use
    void ZoomGradual()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        Camera camera = GetComponent<Camera>();

        // zoom in
        if (zoom > 0)
        {
            // retrieve transforms
            Transform camera_transform = camera.transform;
            Transform zoom_in_transform = firePoint.transform;

            // retrieve positions
            Vector3 camera_position = camera_transform.position;
            Vector3 zoom_in_position = zoom_in_transform.position;

            // move camera to the fire point position
            camera.transform.position = Vector3.Lerp(camera_position, zoom_in_position, cameraTranslationSpeed * Time.deltaTime);
        }
        // zoom out
        else if (zoom < 0)
        {
            // retrieve transforms
            Transform camera_transform = camera.transform;
            Transform zoom_out_transform = zoomOut.transform;

            // retrieve positions
            Vector3 camera_position = camera_transform.position;
            Vector3 zoom_out_position = zoom_out_transform.position;

            // move camera to the storage position
            camera.transform.position = Vector3.Lerp(camera_position, zoom_out_position, cameraTranslationSpeed * Time.deltaTime);
        }
    }
}
