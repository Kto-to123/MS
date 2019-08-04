using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

    Rigidbody rb;
    public float moveSpeed = 5f;
    public float normalSpeed = 5f;
    public float ranSpeed = 10f;
    float dirX;
    float dirY;
    float dirZ;
    // 
    public bool myControl = true;
    //

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = ranSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (myControl)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dirZ = 1 * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dirZ = -1 * moveSpeed;
            }
            else
            {
                dirZ = 0;
            }

            if (Input.GetKey(KeyCode.A))
            {
                dirX = -1 * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dirX = 1 * moveSpeed;
            }
            else
            {
                dirX = 0;
            }
        }
        else
        {
            dirX = Input.GetAxis("Horizontal") * moveSpeed;
            dirZ = Input.GetAxis("Vertical") * moveSpeed;
        }        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX, dirY, dirZ);
    }
}
