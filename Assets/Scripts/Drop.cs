using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс отвечает за объекты лежащие на земле, которые можно подбирать, но они не относятся к оружию
public class Drop : MonoBehaviour
{
    public int id;
    public int count = 1;

    /// <summary>
    /// Взять предмет
    /// </summary>
    public virtual void Take()
    {
        Inventory.instance.TakeItem(id, count);
        Destroy(gameObject);
    }
}
