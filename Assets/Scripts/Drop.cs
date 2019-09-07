using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    //public Inventory inventory;

    //public Item item;
    public int id;

    //private void Start()
    //{
    //    if (inventory == null)
    //    {
    //        inventory = FindObjectOfType<Inventory>();
    //    }
    //}

    public void Take()
    {
        Inventory.instance.TakeItem(id, 1);
        Destroy(gameObject);
    }
}
