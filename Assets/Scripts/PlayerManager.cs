using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

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
        BowScript bow = new BowScript(1);
        Weapon.InstantMainWeapon(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
