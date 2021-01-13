using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateBees : MonoBehaviour
{
    private int noBees = 0;
    private bool isActive = false;
    private Scene activeScene;
    private GameObject beeSample;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        if (activeScene == null)
        {
            print("Cannot find active scene!");
        }
        else
        {
            beeSample = GameObject.Find("FantasyBee");
            if (beeSample == null)
            {
                print("BeeSample not found!");
            }
            else
            {
                switch (activeScene.name)
                {
                    case "Scene1":
                        noBees = 0;
                        break;
                    case "Scene2":
                        noBees = 0;
                        break;
                    case "Scene3":
                        noBees = 0;
                        break;
                    case "Scene4":
                        noBees = 0;
                        isActive = true;
                        break;
                    case "Scene5":
                        noBees = 2;
                        isActive = true;
                        break;
                    case "Scene6":
                        noBees = 5;
                        isActive = true;
                        break;
                    default:
                        noBees = 0;
                        break;
                }
                Vector3 positionBeeSample = beeSample.transform.position;
                //space between bees
                float space = 5.0f;
                for (int i = 0; i < noBees; i++)    //for every bee
                {
                    //position of bee with respect to the SampleBee
                    Vector3 pos = positionBeeSample + new Vector3(-space, 0f, 0f);
                    //generate bees
                    GameObject bee = Instantiate(beeSample, pos, Quaternion.Euler(0, -180, 0));
                    //add script to bee
                    if (isActive)
                    {
                        bee.AddComponent(typeof(BeeFollowPlayer));
                    }
                    //increase space between bees
                    space += 5.0f;
                }

                if (isActive)
                {
                    beeSample.SetActive(true);
                    beeSample.AddComponent(typeof(BeeFollowPlayer));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
