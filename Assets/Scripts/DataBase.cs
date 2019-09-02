using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//База данных м элиментами, заполняемая в ручную
public class DataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

[System.Serializable] //Позволяет получить доступ к скрипту из всего юнити

//Клас с информацией об объекте
public class Item
{
    public int id; // Номер объекта
    public string name; // Имя объекта
    public Sprite img; // Изображение объекта
}
