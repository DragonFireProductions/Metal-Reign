using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneCockpitController : MonoBehaviour
{
    GameObject cockpit;

    public float cockpitRotationSpeed;

    public float cockpitHP;
    public float cockpitArmor;

    float camRayLength = Mathf.Infinity;

    Ray mousePos;

    //Mouse locked check for toggle
    public bool mouseLocked = false;

    void Awake()
    {

    }

    void Start()
    {
        cockpit = gameObject;

        mouseLocked = true;

        if (mouseLocked == true)
        {
            // lock cursor to center of screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        ProcessCockpitMovement();

        MouseUnLock();
    }

    void ProcessCockpitMovement()
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

    void MouseUnLock()
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
    }
}
