using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Базовый класс основного оружияя, сам он не используется, он нужен как интерфейс для взаимодействия с классами наследниками
public class WeaponScript : MonoBehaviour
{
    public int ID;

    public virtual void Reload()
    {

    }

    public virtual void Attack()
    {
        Debug.Log("Это базовый клас, так быть не должно");
    }

    public virtual void AlternativeAttack()
    {
        
    }

    public virtual void InstantiateThis()
    {

    }
}
