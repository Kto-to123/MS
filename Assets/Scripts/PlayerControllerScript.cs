using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер игрока и все связанное с движением
public class PlayerControllerScript : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    public float normalSpeed = 5f;
    public float ranSpeed = 10f;
    public float jumpForce = 10f;

    float dirX;
    float dirY;
    float dirZ;

    Vector3 dirVector;

    public bool myControl = true;
    private Vector3 direction;

    private float distanceToGround;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            dirX = Input.GetAxis("Horizontal") * moveSpeed;
            dirZ = Input.GetAxis("Vertical") * moveSpeed;
        }

        // вектор направления движения
        direction = new Vector3(dirX, rb.velocity.y, dirZ);
        direction = transform.TransformDirection(direction);
        direction = new Vector3(direction.x, rb.velocity.y, direction.z);
    }

    public void Jump()
    {
        if (isGrounded())
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    void FixedUpdate()
    {
        rb.velocity = direction;
    }
}
