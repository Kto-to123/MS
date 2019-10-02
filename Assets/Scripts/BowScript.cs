using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Класс лука, может использоваться как основное оружие
public class BowScript : WeaponScript
{
    public GameObject ActivArrow { get; set; }
    public Transform FierPoint;
    public GameObject arrow;

    ElementMainWeapns Elements;

    public void Start()
    {
        Elements = WeaponDataManagerScript.instance.GetElementMainWeapns(ID);
    }

    public override void Attack()
    {
        Short();
    }

    void Short()
    {
        Instantiate(Elements.arrow, Elements.bowPoint.position, Elements.bowPoint.rotation);
    }
}
