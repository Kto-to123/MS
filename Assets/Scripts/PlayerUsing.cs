using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс отвечает за способнось игрока подбирать лежащие предметы
public class PlayerUsing : MonoBehaviour
{
    private float timeBtwAttack;
    public Transform UsePose;
    public float UseRange;
    public LayerMask whatIsDrop;
    public LayerMask whatIsDropWeapon;
    public LayerMask whatIsDropMainWeapon;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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

            Collider[] dropsToTakeMainWeapon = Physics.OverlapSphere(UsePose.position, UseRange, whatIsDropMainWeapon);
            for (int i = 0; i < dropsToTakeMainWeapon.Length; i++)
            {
                dropsToTakeMainWeapon[i].GetComponent<DropWeapon>().Take();
            }
        }
    }
}
