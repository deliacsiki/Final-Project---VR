using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPerson : MonoBehaviour
{
    public GameObject canvasBtn;
    public GameObject canvasCursor;

    public float turnSpeed = 4.0f;
    public float moveSpeed = 10.0f;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotX;

    private bool disableCameraMovement;

    void Start()
    {
        //rigid body should not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        disableCameraMovement = false;
        canvasCursor.SetActive(true);
        canvasBtn.SetActive(false);
    }

    void Update()
    {

        checkObjectHit(); //for nextLevel btn
        if (!disableCameraMovement)
            MouseAiming();
        KeyboardMovement();
    }

    void MouseAiming()
    {
        // get the mouse inputs
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);

        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }

    void checkObjectHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
        
            var direction = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, 1000))
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.red,5, false);           //make the ray cast visible in scene view

                var colliderName = hit.collider.gameObject.name;
                print(colliderName);
                if (colliderName == "Bee House")
                {
                    Cursor.lockState = CursorLockMode.None;
                    canvasBtn.SetActive(true);
                    canvasCursor.SetActive(false);
                    disableCameraMovement = true;
                }
                else if (colliderName == "NextLevel")
                {
                    SceneManager.LoadScene("Scenes/SecondScene");
                }
            }
        }
    }

    void KeyboardMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(pos.x, 3, pos.z);
    }
}