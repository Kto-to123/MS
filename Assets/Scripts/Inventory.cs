using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    //public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>(); //Список элементов инвентаря

    public GameObject gameObjShow; //Видемый объект?

    public GameObject InventoryMainObject; //Основной объект инвентаря
    public int maxCount; //Количество ячеек инвентаря

    public Camera cam; //Камера
    public EventSystem es; //Управление графическим интерфейсом

    public int currentID; // id перемещаемого предмета
    public ItemInventory currentItem; // перемещаемый предмет

    public RectTransform movingObject; // Переменная для перемещения объекта
    public Vector3 offset; // Смещение от курсора

    public Weapon myWeapon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        //if (data == null)
        //{
        //    data = FindObjectOfType<DataBase>();
        //}

        if (items.Count == 0)
        {
            AddGraphics();
        }

        //Тестовое заполнение инвентаря
        for (int i = 0; i < maxCount; i++)
        {
            AddItem(i, WeaponDataManagerScript.instance.GetElementInventory(Random.Range(0, WeaponDataManagerScript.instance.GetInventoryElementCount())), Random.Range(1, 99));
        }
        UpdateInventory();

        //myWeapon = GetComponent<Weapon>();

        InstantWeapon(1, 51);
    }

    public void Update()
    {
        if (currentID != -1) // Если мы удерживаем объект
        {
            MoveObject(); // Отрисовываем его
        }
    }

    public void TakeItem(int id, int count) // Поднять объект
    {
        ElementInventory qitem = WeaponDataManagerScript.instance.GetElementInventory(id);
        SearchForSameItem(qitem, count);
        
        if (UIManager.instance.backGroundActive)
        {
            UpdateInventory();
        }
    }

    public void SearchForSameItem(ElementInventory item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[i].count < 128)
                {
                    items[i].count += count;

                    if (items[i].count > 128)
                    {
                        count = items[i].count - 128;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }

        if (count > 0)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    public void AddItem(int id, ElementInventory item, int count) //Добавление объекта в инвентарь
    {
        items[id].id = item.id; //Задаем номер
        items[id].count = count; //Задаем количество
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img; //Задаем изображение

        if (count > 1 && item.id != 0) //Если Объект в ячейке не 1 и ячейка не пустая
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString(); //Показать количество объектов
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem) //Добавление ячеек инвентаря
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(invItem.id).img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics() //Отрисовка инвентаря
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject; //Создание видимого объекта

            newItem.name = i.ToString(); //Объект получает имя равное его порядковому номеру

            ItemInventory ii = new ItemInventory(); //Реализуем ячейку инвентаря
            ii.itemGameObj = newItem; //Записываем ссылку на объект

            RectTransform rt = newItem.GetComponent<RectTransform>(); //Информация о положении, размере, привязке и опоре для прямоугольника
            rt.localPosition = new Vector3(0, 0, 0); //Текущее положение
            rt.localScale = new Vector3(1, 1, 1); //Текущий размер
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1); //Масштаб при использовании

            Button tempbutton = newItem.GetComponent<Button>(); //Придание свойств кнопки объекту

            tempbutton.onClick.AddListener(delegate { SelectObject(); }); // Создание слушателя нажатия кнопки?

            items.Add(ii); //Добавление объекта в список элементов инвентаря
        }
    }

    public void UpdateInventory() // Обновление графики инвентаря
    {
        for (int i = 0; i < maxCount; i++) // Пройтись по каждой клетке
        {
            if (items[i].id != 0 && items[i].count > 1) // Если в ней есть объект и его количество больше 0
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString(); // Отрисовываем количество
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = ""; // Отрисовываем пустую строку
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(items[i].id).img; // Отрисовываем изображение соответствующее ID
        }
    }

    public void SelectObject() //Перемещение объекта
    {
        if (currentID == -1) // Если объект еще не взят, берем
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true); //Видимость перемещаемого объекта
            movingObject.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id).img; //Присвоение перемещаемому объекту изображения

            AddItem(currentID, WeaponDataManagerScript.instance.GetElementInventory(0), 0); //Записываем в ячейку пустой элемент
        }
        else // если уже взят опускаем
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                AddInventoryItem(currentID, II);
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= 128)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, WeaponDataManagerScript.instance.GetElementInventory(II.id), II.count + currentItem.count - 128);

                    II.count = 128;
                }

                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }

            currentID = -1; // Делаем счетчик пустым

            movingObject.gameObject.SetActive(false); // Скрываем переносимый объект
        }
    }

    public void MoveObject() // Перемещение объекта курсором
    {
        Vector3 pos = Input.mousePosition + offset; // Получаем позицию курсора + смещение
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z; 
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old) //Копирование содержимоко ячейки в буферную переменную
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj; 
        New.count = old.count;

        return New;
    }

    // Взаимодействие с оружием
    public void InstantWeapon(int WId, int WCount)
    {
        myWeapon.InstantWeapon(WId, WCount);
    }
}

[System.Serializable] //Позволяет получить доступ к скрипту из всего юнити

// Ячейка инвентаря
public class ItemInventory
{
    public int id; //Номер ячейки
    public GameObject itemGameObj; //Ссылка на содержимое

    public int count; //Количество предметов в ячейке
}