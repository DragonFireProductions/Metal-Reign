using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThroneTreadController : MonoBehaviour
{
    //[HideInInspector]
    public float curSpeed;
    public float maxSpeed;
    public float reverseMaxSpeed;

    public float acceleration;
    public float deceleration;
    public float brakeForce;

    public float chassisRotationSpeed;

    public float chassisHP;
    public float chassisArmor;

    bool reverseReady;

    Rigidbody chassisRB;

    void Start()
    {
        chassisRB = gameObject.GetComponent<Rigidbody>();
        reverseReady = false;
    }

    void Update()
    {
        ProcessMovement();
    }

    void LateUpdate()
    {
        chassisRB.velocity = transform.forward * curSpeed;

        //Deceleration when no keys are pressed
        if (!Input.anyKey)
        {
            //Converting braking force into deceleration
            curSpeed -= deceleration * Time.deltaTime;
            //Constrain speed between current speed and 0
            curSpeed = Mathf.Max(curSpeed, 0);
            //chassisRB.velocity = transform.forward * curSpeed;
        }

        reverseReady = false;
    }

    void ProcessMovement()
    {
        float moveY = Input.GetAxisRaw("Horizontal");
        float MoveZ = Input.GetAxisRaw("Vertical");

        Transform chassisTransform = gameObject.GetComponent<Transform>();

        //NOTE: CREATE INPUT MANAGER AND REPLACE GET KEY CHECK
        //Forward Movement with acceleration
        if (Input.GetKey(KeyCode.W) && curSpeed < maxSpeed)
        {
            //Converting acceleration into speed
            curSpeed += acceleration * Time.deltaTime;
            //Constrain speed between current speed and max speed
            curSpeed = Mathf.Min(curSpeed, maxSpeed);
        }

        //NOTE: CREATE INPUT MANAGER AND REPLACE GET KEY CHECK
        //Braking with strong deceleration
        if (Input.GetKey(KeyCode.S) && curSpeed > -maxSpeed)
        {
            //Converting braking force into deceleration
            curSpeed -=  brakeForce * Time.deltaTime;
            //Constrain speed between current speed and 0
            curSpeed = Mathf.Max(curSpeed, 0);
        }

        //Left/Right rotation
        if (moveY != 0)
        {
            chassisTransform.Rotate(0.0f, moveY * chassisRotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }
    }
}
