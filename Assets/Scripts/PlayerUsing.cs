using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUsing : MonoBehaviour
{
    private float timeBtwAttack;
    //public float startTimeBtwAttack;

    public Transform UsePose;
    public float UseRange;
    public LayerMask whatIsDrop;
    public LayerMask whatIsDropWeapon;
    //public int damage;

    void Update()
    {
        //if (timeBtwAttack <= 0)
        //{
        if (Input.GetKey(KeyCode.E))
        {
            Collider[] dropsToTake = Physics.OverlapSphere(UsePose.position, UseRange, whatIsDrop);
            for (int i = 0; i < dropsToTake.Length; i++)
            {
                dropsToTake[i].GetComponent<Drop>().Take();
            }

            Collider[] dropsToTakeWeapon = Physics.OverlapSphere(UsePose.position, UseRange, whatIsDropWeapon);
            for (int i = 0; i < dropsToTakeWeapon.Length; i++)
            {
                dropsToTakeWeapon[i].GetComponent<DropWeapom>().Take();
            }
        }

        //    timeBtwAttack = startTimeBtwAttack;
        //}
        //else
        //{
        //    timeBtwAttack -= Time.deltaTime;
        //}
    }
}
