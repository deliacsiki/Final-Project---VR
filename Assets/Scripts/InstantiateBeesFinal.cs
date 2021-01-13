using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateBeesFinal : MonoBehaviour
{
    private int noBees = 30;
    private Scene activeScene;
    private GameObject beeSample;
    
    private float radiusOuterCircle = 6f;
    private float radiusInnerCircle = 1.5f;
    
    private List<GameObject> listOfBees = new List<GameObject>();
    private GameObject player;

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
                player = GameObject.Find("Player");
                if (player == null)
                {
                    print("BeeSample not found!");
                }
                else
                {
                    //position of player
                    Vector3 positionPlayer = player.transform.position;
                    //position of bee sample
                    Vector3 positionBeeSample = beeSample.transform.position;

                    //space between bees
                    float spaceRow = 3.0f;
                    float depth = 2.0f;
                    for (int i = 0; i < 20; i++) //for every bee
                    {
                        //position of bee with respect to the SampleBee
                        Vector3 posRight = positionPlayer + new Vector3(spaceRow / 2, positionBeeSample.y - positionPlayer.y, depth);
                        Vector3 posLeft = positionPlayer + new Vector3(-spaceRow / 2, positionBeeSample.y - positionPlayer.y, depth);
                        //generate bees
                        GameObject beeRight = Instantiate(beeSample, posRight, Quaternion.Euler(0, 270, 0));
                        GameObject beeLeft = Instantiate(beeSample, posLeft, Quaternion.Euler(0, 90, 0));
                        //make bees stay still
                        beeRight.AddComponent(typeof(BeeStayStill));
                        beeLeft.AddComponent(typeof(BeeStayStill));
                        //increase space between bees
                        depth += 2.0f;
                        
                        listOfBees.Add(beeRight);
                        listOfBees.Add(beeLeft);
                    }
                    GameObject beeHouse = GameObject.Find("Bee House");
                    if (beeHouse != null)
                    {
                        Vector3 beeHousePosition = beeHouse.transform.position;

                        //for outer cirlce
                        int beesInOuterCircle = 20;
                        
                        //put bees in outer circle around bee house
                        for (int i = 0; i < beesInOuterCircle; i++)
                        {
                            if (i != 15)
                            {
                                float angle = i * Mathf.PI * 2 / beesInOuterCircle;
                                float x = Mathf.Cos(angle) * radiusOuterCircle;
                                float z = Mathf.Sin(angle) * radiusOuterCircle;
                                Vector3 pos = beeHousePosition +
                                              new Vector3(x, positionBeeSample.y - beeHousePosition.y, z);
                                float angleDegrees = -angle * Mathf.Rad2Deg;
                                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                                GameObject bee = Instantiate(beeSample, pos, rot);
                                bee.AddComponent(typeof(BeeStayStill));
                                
                                listOfBees.Add(bee);
                            }
                        }
                        
                        //for inner circle
                        int beesInInnerCircle = 5;
                        
                        //put bees in inner circle around bee house
                        for (int i = 0; i < beesInInnerCircle; i++)
                        {
                            float angle = i * Mathf.PI * 2 / beesInInnerCircle;
                            float x = Mathf.Cos(angle) * radiusInnerCircle;
                            float z = Mathf.Sin(angle) * radiusInnerCircle;
                            Vector3 pos = beeHousePosition + new Vector3(x, positionBeeSample.y - beeHousePosition.y, z);
                            float angleDegrees = -angle * Mathf.Rad2Deg;
                            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                            GameObject bee = Instantiate(beeSample, pos, rot);
                            bee.AddComponent(typeof(BeeStayStill));
                            
                            listOfBees.Add(bee);
                        }
                    }
                    else
                    {
                        print("Be House not found!");
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var bee in listOfBees)
        {
            bee.transform.LookAt(player.transform);
        }
    }
}
