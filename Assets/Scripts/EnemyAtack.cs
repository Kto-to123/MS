using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public GameObject Hammer;
    //Animator hammerAnimator;
    WeaponManager weapon;

    private void Start()
    {
        //hammerAnimator = Hammer.GetComponent<Animator>();
        weapon = transform.parent.GetComponent<WeaponManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            weapon.MainAttack();
            //hammerAnimator.SetTrigger("Attack");
    }
}
