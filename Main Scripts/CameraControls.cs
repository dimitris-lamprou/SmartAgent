using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Vector3 cameraPos;

    private float fov = 60f;
    private float minFov = 10f;
    private float maxFov = 70f;

    [SerializeField] float mouseWheelSensitivity = 10f;
    [SerializeField] float cameraSpeed = 10f;
    [SerializeField] float cameraShiftSpeed = 20f;
    [SerializeField] float cameraWithoutShiftSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = this.transform.position;
        cameraShiftSpeed = cameraSpeed * 2;
        cameraWithoutShiftSpeed = cameraSpeed;

        fov = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraSpeed = cameraShiftSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            cameraSpeed = cameraWithoutShiftSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.z > 100)
            {

            }
            else
            {
                cameraPos.z += cameraSpeed / 50;
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        {
            if (transform.position.z < 0)
            {

            }
            else
            {
                cameraPos.z -= cameraSpeed / 50;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x > 100)
            {

            }
            else
            {
                cameraPos.x += cameraSpeed / 50;
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x < -10)
            {

            }
            else
            {
                cameraPos.x -= cameraSpeed / 50;
            }
        }


        this.transform.position = cameraPos;

        fov -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
