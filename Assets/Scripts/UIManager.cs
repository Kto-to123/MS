using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject m1;
    public GameObject m2;
    public GameObject m3;
    public Text text;

    public GameObject backGround;


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
    //void Start()
    //{
    //    inventory = FindObjectOfType<Inventory>();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                Inventory.instance.UpdateInventory();
            }
        }
    }

    public void SetAmmo(int ammo)
    {
        text.text = ammo.ToString();
    }

    public void SetWeapon(int WeaponID, int ammo)
    {
        switch (WeaponID)
        {
            case 0:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(false);
                text.text = ammo.ToString();
                break;
            case 1:
                m1.SetActive(true);
                m2.SetActive(false);
                m3.SetActive(false);
                text.text = ammo.ToString();
                break;
            case 2:
                m1.SetActive(false);
                m2.SetActive(true);
                m3.SetActive(false);
                text.text = ammo.ToString();
                break;
            case 3:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(true);
                text.text = ammo.ToString();
                break;
            default:
                m1.SetActive(false);
                m2.SetActive(false);
                m3.SetActive(false);
                text.text = ammo.ToString();
                break;
        }
    }
}
