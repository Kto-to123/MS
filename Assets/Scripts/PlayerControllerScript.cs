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
    bool jumpBackActiv = true;

    public float bfx = 1;
    public float bfy = 2;

    float dirX;
    float dirY;
    float dirZ;

    Vector3 dirVector;

    public bool myControl = true;
    private Vector3 direction;

    private float distanceToGround;

    // Управление инвентарем
    bool inventoryActiv = true;
    // Управление меню прогрессии
    bool skillUIActiv = false;
    //

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
    }

    void UIClose()
    {
        Cursor.visible = false;
        inventoryActiv = false;
        UIManager.instance.ActivateInterfaceInventory(false);
        skillUIActiv = false;
        UIManager.instance.ActivateInterfaceSkillProgras(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryActiv)
            {
                UIClose();
                inventoryActiv = true;
                UIManager.instance.ActivateInterfaceInventory(inventoryActiv);
                Cursor.visible = inventoryActiv;
            }
            else
            {
                UIClose();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!skillUIActiv)
            {
                UIClose();
                skillUIActiv = true;
                UIManager.instance.ActivateInterfaceSkillProgras(skillUIActiv);
                Cursor.visible = skillUIActiv;
            }
            else
            {
                UIClose();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIClose();
        }

        // Проверка нажатий клавиш стрельбы
        if (!inventoryActiv)
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
            JumpBack();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Squatting(1);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Squatting(2);
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
