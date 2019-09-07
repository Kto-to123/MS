using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject m1;
    public GameObject m2;
    public GameObject m3;

    public GameObject backGround;
    private Inventory inventory;

    private bool weaponActive = true;
    private int myAmmunition = 0;

    #region Structur
    public bool backGroundActive {
        get {
            return backGround.activeSelf;
        }

        set {
            backGround.SetActive(value); 
        }
    }
    #endregion

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

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                inventory.UpdateInventory();
            }
        }
    }

    public void SetWeapon(int WeaponID)
    {
        switch (WeaponID)
        {
            case 0:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(false);
                break;
            case 1:
                m1.SetActive(true);
                m2.SetActive(false);
                m3.SetActive(false);
                break;
            case 2:
                m1.SetActive(false);
                m2.SetActive(true);
                m3.SetActive(false);
                break;
            case 3:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(true);
                break;
            default:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(false);
                break;
        }
    }
}
