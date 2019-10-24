using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponScript
{
    public Transform fierPoint;
    public GameObject Dec;
    public int damage = 1;
    public ParticleSystem Partial;

    private void Start()
    {
        Partial.Stop();
    }

    public override void Attack()
    {
        ElementInventory usebl = WeaponDataManagerScript.instance.GetElementInventory(Inventory.instance.mainAmmunitionSlot.id);
        if (usebl.ammoType == AmmoType.bullet && Inventory.instance.mainAmmunitionSlot.count > 0)
        {
            Inventory.instance.mainAmmunitionSlot.count--;
            Short();
        }
    }

    void Short()
    {
        Ray ray = new Ray(fierPoint.position, fierPoint.forward * 10f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 500f, 1, QueryTriggerInteraction.Ignore))
        {
            Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            GameObject g = Instantiate<GameObject>(Dec);
            g.transform.position = hit.point + hit.normal * 0.01f;
            g.transform.rotation = Quaternion.LookRotation(-hit.normal);
            g.transform.SetParent(hit.transform);
            Rigidbody r = hit.transform.gameObject.GetComponent<Rigidbody>();
            if(r != null)
            {
                r.AddForceAtPosition(-hit.normal * 500, hit.point);
            }
        }
        Partial.Play();
    }
}
