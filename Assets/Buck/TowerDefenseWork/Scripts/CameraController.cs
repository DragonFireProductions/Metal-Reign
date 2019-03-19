using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool doMovement = true;

    [SerializeField]
    float panSpeed;

    //Used to determine how many pixels away from the border of the 
    //Screen the mouse is to move the camera
    [SerializeField]
    float panBorderaThickness;
	
	// Update is called once per frame
	void Update ()
    {
        //If escape is pressed set the bool to the
        //Opposite of what it is
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        //If false don't move camera
        if (!doMovement)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
    }
}
