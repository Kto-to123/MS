using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер игрока и все связанное с движением
public class PlayerControllerScript : MonoBehaviour
{
    public static PlayerControllerScript instance;

    Rigidbody rb;
    CapsuleCollider capsCollider;
    public float moveSpeed = 5f;
    public float normalSpeed = 5f;
    public float ranSpeed = 10f;
    public float jumpForce = 10f;
    public float jumpBackCooldown = 0.3f;
    bool jumpBackActiv = true; // Показывает что персоонаж не находится в полете после прыжка

    // Переменные для настройки прыжка назад
    public float bfx = 1;
    public float bfy = 2;

    [SerializeField] float dirX;
    [SerializeField] float dirY;
    [SerializeField] float dirZ;

    Vector3 dirVector;

    public bool myControl = true;
    [SerializeField] private Vector3 direction;

    private float distanceToGround;

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
            Weapon.instance.TrapSetting();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Weapon.instance.TrapIstanse();
        }

        // Проверка нажатий клавиш стрельбы
        if (UIManager.instance.activUImode == UImode.Game)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Weapon.instance.ThrowingAttack();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Weapon.instance.MainAttack();
            }
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

        // вектор направления движения
        direction = new Vector3(dirX, rb.velocity.y, dirZ);
        direction = transform.TransformDirection(direction);
        //direction = new Vector3(direction.x + (rb.velocity.x - moveSpeed), direction.y, direction.z + (rb.velocity.z - moveSpeed));
    }

    public void SetMoveSpeed(float _Speed)
    {
        normalSpeed = _Speed;
        ranSpeed = normalSpeed * 2;
    }

    void Squatting(int _CapsCol)
    {
        capsCollider.height = _CapsCol;
    }

    void Jump()
    {
        if (isGrounded())
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

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
    }

    void FixedUpdate()
    {
        rb.velocity = direction;
    }
}