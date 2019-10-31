using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс отвечает за способнось игрока подбирать лежащие предметы
public class PlayerUsing : MonoBehaviour
{
    private float timeBtwAttack;
    public Transform UsePose;
    public float UseRange;
    LayerMask whatIsDrop;

    private void Start()
    {
        whatIsDrop = LayerMask.GetMask("Drop");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] dropsToTake = Physics.OverlapSphere(UsePose.position, UseRange, whatIsDrop);
            for (int i = 0; i < dropsToTake.Length; i++)
            {
                dropsToTake[i].GetComponent<Drop>().Take();
            }
        }
    }
}
