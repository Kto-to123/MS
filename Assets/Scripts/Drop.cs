using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public int id;

    public void Take()
    {
        Inventory.instance.TakeItem(id, 1);
        Destroy(gameObject);
    }
}
