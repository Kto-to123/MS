using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    //public DataBase data;

    public Inventory inventory;

    //public Item item;
    public int id;
    
    public void Take()
    {
        inventory.TakeItem(id, 1);

        Destroy(gameObject);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
