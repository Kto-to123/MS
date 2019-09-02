using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Inventory inventory;

    //public Item item;
    public int id;
    
    public void Take()
    {
        inventory.TakeItem(id, 1);

        Destroy(gameObject);
    }
}
