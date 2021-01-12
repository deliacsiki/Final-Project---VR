using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFollowPlayer : MonoBehaviour
{
    private bool activated = false;
    private float barrierDistance = 5.0f;
    private float movementSpeed = 2.0f;
    private GameObject player = null;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(("Player"));
        if (player == null)
        {
            print("Player not found");
        }

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = GetDistanceToPlayer();
        transform.LookAt(player.transform.position);
        if (distanceToPlayer >= barrierDistance)    //we are not to close
        {
            //move
            anim.Play("Move");
            transform.position += (transform.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            //not moving
            anim.Play("Idle");
        }
    }

    private float GetDistanceToPlayer()
    {
        if (player != null)
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
        print("Player not found");
        return 0.0f;
    }
}
