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

    //managing time
    private float startTime;

    private Coroutine coroutine = null;
    private Scene activeScene;

    private List<GameObject> bees;
    
    private Rigidbody rb;

    void Start()
    {
        //rigid body should not change rotation
        if (GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;

        canvasBtn = GameObject.Find("CanvasBtn");
        canvasCursor = GameObject.Find("CanvasCursor");

        disableCameraMovement = false;
        if (canvasCursor)
        {
            canvasCursor.SetActive(true);
        }

        if (canvasBtn)
        {
            canvasBtn.SetActive(false);
        }

        // activeScene = SceneManager.GetActiveScene();
        // if (activeScene.name != "SecondScene" && activeScene.name != "FirstScene")
        // {
        //     bees = new List<GameObject>();
        //     GameObject bee = GameObject.Find("FantasyBee");
        //     print("bee: " + bee);
        //     if (bee)
        //     {
        //         bees.Add(bee);
        //         foreach (GameObject beeItem in bees)
        //         {
        //             beeItem.SetActive(false);
        //         }
        //     }
        // }
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
            if (Physics.Raycast(transform.position, direction, out hit, 4.5f))
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.red, 5, false);           //make the ray cast visible in scene view

                var colliderName = hit.collider.gameObject.name;
                print(colliderName);
                if (colliderName == "Bee House")        //bee house hit
                {
                    if (activeScene.name == "Scene1")
                    {
                        coroutine = StartCoroutine(NextLevelCoroutine(0));
                    }
                    else
                    {
                        coroutine = StartCoroutine(NextLevelCoroutine(5));
                        // if (activeScene.name != "Scene2")
                        // {
                        //     foreach (GameObject bee in bees)
                        //     {
                        //         bee.SetActive(true);
                        //     }
                        // }
                    }
                }
            }
        }
    }


    IEnumerator NextLevelCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        Cursor.lockState = CursorLockMode.None;
        canvasBtn.SetActive(true);
        canvasCursor.SetActive(false);
        disableCameraMovement = true;
    }

    void KeyboardMovement()
    {
        Vector3 pos = transform.position;

        if ((Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a")) && coroutine != null)
        {
            print("You have to stay put after touching the bee house");
            StopCoroutine(coroutine);
        }

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
    
    private void OnCollisionStay(Collision other)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}