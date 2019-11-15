using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер игрока и все связанное с движением
public class PlayerControllerScript : MonoBehaviour
{
    public static PlayerControllerScript instance;

    Rigidbody rb;
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    CapsuleCollider capsCollider;
    public float gravity = -9.81f;
    public float moveSpeed = 5f;
    public float normalSpeed = 5f;
    public float ranSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpBackCooldown = 0.3f;
    bool jumpBackActiv = true; // Показывает что персоонаж не находится в полете после прыжка
    bool stairsUsing = false; // Использование лестницы

    // Переменные для настройки прыжка назад
    public float bfx = 1;
    public float bfy = 2;

    [SerializeField] float dirX;
    [SerializeField] float dirY;
    [SerializeField] float dirZ;

    Vector3 dirVector;
    Vector3 oldVector;
    Vector3 newVector;
    Vector3 velocity;

    public bool castomControlInput = true; 
    [SerializeField] Vector3 direction;

    private float distanceToGround;
    bool isGraunded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        capsCollider = GetComponent<CapsuleCollider>();

        UIManager.instance.SetUIMode(UImode.Game);
    }

    void Update()
    {
        isGraunded = Physics.CheckSphere(groundCheck.position, 0.4f, 0);

        if (Input.GetKeyDown(KeyCode.Tab))
        {            
            UIManager.instance.SetUIMode(UImode.Inventory);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            UIManager.instance.SetUIMode(UImode.Skills);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.SetUIMode(UImode.Game);
        }

        if (Input.GetKey(KeyCode.F))
        {
            PlayerManager.instance.TrapSetting();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            PlayerManager.instance.TrapIstanse();
        }

        // Проверка нажатий клавиш стрельбы
        if (UIManager.instance.activUImode == UImode.Game)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PlayerManager.instance.ThrowingAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerManager.instance.AttackMainWeapon();
            }
        }

        if (Input.GetKey(KeyCode.G))
        {
            PlayerManager.instance.MainAlternativeAttack();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = ranSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.X)) // Отскок назад
        {
            //JumpBack();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Squatting(1);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Squatting(2);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (castomControlInput)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

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
            rb.isKinematic = true;
            rb.useGravity = false;

            if (isGraunded && velocity.y > 0)
            {
                velocity.y = -2f;
            }

            dirX = Input.GetAxis("Horizontal");
            dirZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * dirX + transform.forward * dirZ;

            controller.Move(move * moveSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
    }

    public void SetMoveSpeed(float _Speed)
    {
        normalSpeed = _Speed;
        ranSpeed = normalSpeed * 2;
    }

    // Перемещение по лестницам
    public void StairsSetUsing(bool _using)
    {
        stairsUsing = _using;
        if (_using)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    // Приседания
    void Squatting(int _CapsCol)
    {
        capsCollider.height = _CapsCol;
    }
    
    // Прыжок
    void Jump()
    {
        if (IsGrounded())
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
    }

    void JumpBack()
    {
        //if (jumpBackActiv)
        //{
            jumpBackActiv = false;
            rb.AddForce((transform.up * (jumpForce * bfx) + (transform.forward * (jumpForce * bfy) * -1)), ForceMode.VelocityChange);
            Invoke("JumpBackCooldown", jumpBackCooldown);
        //}
    }

    void JumpBackCooldown()
    {
        jumpBackActiv = true;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    void FixedUpdate()
    {
        if (castomControlInput)
        {
            if (stairsUsing)
            {
                var v = new Vector3(dirX, dirZ, 0);
                rb.velocity = v;
            }
            else
            {
                direction = rb.velocity;

                if (rb.velocity.magnitude <= ranSpeed)
                {
                    Vector3 vel = velocity;
                    Vector3 dir = (transform.right * dirX + transform.forward * dirZ).normalized * moveSpeed;
                    //direction = new Vector3(rb.velocity.x + dirX, rb.velocity.y, rb.velocity.z + dirZ);
                    direction = vel + dir;
                    direction.y = rb.velocity.y;
                    //direction = transform.TransformDirection(direction);
                }
                else
                {
                    if (dirX != 0 || dirZ != 0)
                    {
                        rb.velocity = rb.velocity * 0.9f + (transform.right * dirX + transform.forward * dirZ).normalized * moveSpeed;
                    }
                }

                rb.velocity = direction;
            }
        }
    }
}