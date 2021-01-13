using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateBeesFinal : MonoBehaviour
{
    private int noBees = 30;
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
                GameObject player = GameObject.Find("Player");
                if (player == null)
                {
                    print("BeeSample not found!");
                }
                else
                {
                    //position of player
                    Vector3 positionPlayer = player.transform.position;
                    Vector3 positionBeeSample = beeSample.transform.position;

                    //space between bees
                    float spaceRow = 3.0f;
                    float depth = 2.0f;
                    for (int i = 0; i < noBees - 10; i++) //for every bee
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
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
