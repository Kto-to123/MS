using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public GameObject Hammer;
    Animator hammerAnimator;

    private void Start()
    {
        hammerAnimator = Hammer.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            hammerAnimator.SetTrigger("Attack");
    }
}
