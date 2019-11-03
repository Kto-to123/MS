using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    /// <summary>
    /// Список элементов инвентаря
    /// </summary>
    public List<ItemInventory> items = new List<ItemInventory>();

    // Слоты экипировки
    public ItemInventory mainWeaponSlot;
    public ItemInventory mainAmmunitionSlot;
    public ItemInventory throwingWeaponSlot;
    public ItemInventory shlemSlot;
    public ItemInventory dospehSlot;
    public ItemInventory perchatkiSlot;
    public ItemInventory poyasSlot;
    public ItemInventory shtaniSlot;
    public ItemInventory ObyvSlot;

    /// <summary>
    /// Видемый объект
    /// </summary>
    [SerializeField] GameObject gameObjShow;

    /// <summary>
    /// Основной объект инвентаря
    /// </summary>
    [SerializeField] GameObject InventoryMainObject;

    /// <summary>
    /// Количество ячеек инвентаря
    /// </summary>
    [SerializeField] int maxCount;

    /// <summary>
    /// Управление графическим интерфейсом
    /// </summary>
    EventSystem es;

    /// <summary>
    /// Ячейка перемещаемого предмета
    /// </summary>
    int cellCurrentID = -1;

    /// <summary>
    /// перемещаемый предмет
    /// </summary>
    public ItemInventory currentItem;

    /// <summary>
    /// Переменная для перемещения объекта
    /// </summary>
    public RectTransform movingObject;

    /// <summary>
    /// Смещение от курсора
    /// </summary>
    public Vector3 offset;

    void Awake()
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

        es = FindObjectOfType<EventSystem>();
    }

    void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }

        UpdateInventory();

        TakeItem(10, 1);
        TakeItem(13, 1);
        TakeItem(16, 1);
        TakeItem(19, 1);
        TakeItem(21, 1);
        TakeItem(22, 50);
        TakeItem(23, 50);
    }

    void Update()
    {
        if (cellCurrentID != -1) // Если мы удерживаем объект
        {
            MoveObject(); // Отрисовываем его
        }
    }

    /// <summary>
    /// Добавить объект в инвентарь
    /// </summary>
    /// <param name="id">ID Добавляемого объекта</param>
    /// <param name="count">Количество объектов</param>
    public void TakeItem(int id, int count) 
    {
        ElementInventory qitem = WeaponDataManagerScript.instance.GetElementInventory(id);
        SearchForSameItem(qitem, count);
        
        if (UIManager.instance.backGroundActive)
        {
            UpdateInventory();
        }
    }

    /// <summary>
    /// Добавление элемента в свободную ячейку, или добавление нужного количества объектов в ячейку с таким-же объектом
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    void SearchForSameItem(ElementInventory item, int count)
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

    /// <summary>
    /// Добавление ячейки
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <param name="count"></param>
    void AddItem(int id, ElementInventory item, int count)
    {
        items[id].id = item.id; // Задаем номер
        items[id].count = count; // Задаем количество
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img; // Задаем изображение
        items[id].element = item; // Добавляем ссылку на содержимое ячейки

        if (count > 1 && item.id != 0) // Если Объект в ячейке не 1 и ячейка не пустая
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString(); // Показать количество объектов
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    /// <summary>
    /// Забыл что это за функция, но она нужна!
    /// </summary>
    /// <param name="id"></param>
    /// <param name="invItem"></param>
    void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(invItem.id).img;
        items[id].element = invItem.element; // Добавляем ссылку на содержимое ячейки

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    /// <summary>
    /// Отрисовка инвентаря
    /// </summary>
    void AddGraphics()
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

    /// <summary>
    /// Обновление графики инвентаря
    /// </summary>
    public void UpdateInventory()
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

        UpdateEquipmentSlots(mainWeaponSlot);
        UpdateEquipmentSlots(mainAmmunitionSlot);
        UpdateEquipmentSlots(throwingWeaponSlot);
        UpdateEquipmentSlots(shlemSlot);
        UpdateEquipmentSlots(dospehSlot);
        UpdateEquipmentSlots(perchatkiSlot);
        UpdateEquipmentSlots(poyasSlot);
        UpdateEquipmentSlots(shtaniSlot);
        UpdateEquipmentSlots(ObyvSlot);
    }

    /// <summary>
    /// Обновление графики слота экипировки
    /// </summary>
    /// <param name="slot"></param>
    void UpdateEquipmentSlots(ItemInventory slot)
    {
        if (slot.id != 0 && slot.count > 1)
        {
            slot.itemGameObj.GetComponentInChildren<Text>().text = slot.count.ToString();
            slot.itemGameObj.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(slot.id).img;
        }
        else if (slot.id != 0 && slot.count == 1)
        {
            slot.itemGameObj.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(slot.id).img;
            slot.itemGameObj.GetComponentInChildren<Text>().text = "";
        }
        else
        {
            slot.itemGameObj.GetComponentInChildren<Text>().text = "";
            slot.itemGameObj.GetComponent<Image>().sprite = slot.standartSprite;
        }
    }

    /// <summary>
    /// Перемещение объекта
    /// </summary>
    void SelectObject()
    {
        if (cellCurrentID == -1) // Если объект еще не взят, берем
        {
            int _cellCurrentID = int.Parse(es.currentSelectedGameObject.name); // Получаем ID ячейки
            ItemInventory _currentItem = CopyInventoryItem(items[_cellCurrentID]); // Копируем объект в ячейку для переноса
            if (_currentItem.id != 0)
            {
                cellCurrentID = _cellCurrentID;
                currentItem = _currentItem;

                movingObject.gameObject.SetActive(true); // Видимость перемещаемого объекта
                movingObject.GetComponent<Image>().sprite = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id).img; // Присвоение перемещаемому объекту изображения
                AddItem(cellCurrentID, WeaponDataManagerScript.instance.GetElementInventory(0), 0); // Записываем в ячейку пустой элемент
            }
        }
        else // если уже взят опускаем
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                AddInventoryItem(cellCurrentID, II);
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
                    AddItem(cellCurrentID, WeaponDataManagerScript.instance.GetElementInventory(II.id), II.count + currentItem.count - 128);

                    II.count = 128;
                }

                if (II.id != 0)
                    II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }

            cellCurrentID = -1; // Делаем счетчик пустым

            movingObject.gameObject.SetActive(false); // Скрываем переносимый объект
        }
    }

    /// <summary>
    /// Отрисовка перемещаемого объекта
    /// </summary>
    void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = pos;// cam.ScreenToWorldPoint(pos);
    }

    /// <summary>
    /// Копирование содержимоко ячейки в буферную переменную
    /// </summary>
    /// <param name="old"></param>
    /// <returns></returns>
    ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj; 
        New.count = old.count;

        return New;
    }

    /// <summary>
    /// Расчет брони
    /// </summary>
    public void SetDefens()
    {
        int armor = 0;
        if(shlemSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(shlemSlot.id).equipment.armor;
        if (dospehSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(dospehSlot.id).equipment.armor;
        if (perchatkiSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(perchatkiSlot.id).equipment.armor;
        if (poyasSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(poyasSlot.id).equipment.armor;
        if (shtaniSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(shtaniSlot.id).equipment.armor;
        if (ObyvSlot.id != 0)
            armor += WeaponDataManagerScript.instance.GetElementInventory(ObyvSlot.id).equipment.armor;
        PlayerManager.instance.SetArmor(armor);
    }

    #region Функции для Слотов экипировки
    /// <summary>
    /// Функция для слота основного оружия
    /// </summary>
    public void MainWeaponDragAndDrop()
    {
        int usebl = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id).mainWeapon;
        if (currentItem.id > 0 && usebl > 0 && cellCurrentID != -1)
        {
            DragAndDropWeaponSlot(mainWeaponSlot);
            Weapon.instance.InstantMainWeapon(usebl);
        }
    }

    /// <summary>
    /// Функция для слота метательного оружия
    /// </summary>
    public void ThrowingWeaponDragAndDrop()
    {
        int usebl = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id).throwingWeapon;
        if (currentItem.id > 0 && usebl > 0 && cellCurrentID != -1)
        {
            DragAndDropWeaponSlot(throwingWeaponSlot);
            Weapon.instance.InstantWeapon(usebl, throwingWeaponSlot.count);
        }
    }

    /// <summary>
    /// Функция для слота шлема
    /// </summary>
    public void shlemSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Shlem, shlemSlot);
    }

    /// <summary>
    /// Функция для слота доспехов
    /// </summary>
    public void dospehSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Dospeh, dospehSlot);
    }

    /// <summary>
    /// Функция для слота перчаток
    /// </summary>
    public void perchatkiSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Perchatki, perchatkiSlot);
    }

    /// <summary>
    /// Функция для слота пояса
    /// </summary>
    public void poyasSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Poyas, poyasSlot);
    }

    /// <summary>
    /// Функция для слота штанов
    /// </summary>
    public void shtaniSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Shtani, shtaniSlot);
    }

    /// <summary>
    /// Функция для слота обуви
    /// </summary>
    public void ObyvSlotDragAndDrop()
    {
        InstallabilityCheck(EquipmanType.Obyv, ObyvSlot);
    }

    /// <summary>
    /// Функция для слота боеприпасов
    /// </summary>
    public void MainAmmoSlotDragAndDrop()
    {
        ElementInventory usebl = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id);

        if (currentItem.id > 0 && cellCurrentID != -1 && usebl.ammoType != AmmoType.now)
        {
            DragAndDropWeaponSlot(mainAmmunitionSlot);
        }
    }
    #endregion

    /// <summary>
    /// Проверка возможности установить в слот выбранное снаряжение
    /// </summary>
    /// <param name="_EquipmanType">Снаряжение которое можно установить слот</param>
    /// <param name="_slot">Слот для снаряжения</param>
    void InstallabilityCheck(EquipmanType _EquipmanType, ItemInventory _slot)
    {
        Equipment usebl = WeaponDataManagerScript.instance.GetElementInventory(currentItem.id).equipment;

        if (currentItem.id > 0 && cellCurrentID != -1 && usebl != null)
        {
            if (usebl.type == _EquipmanType)
            {
                DragAndDropWeaponSlot(_slot);
                SetDefens();
            }
        }
    }

    /// <summary>
    /// Перетаскивание экипировки в слот
    /// </summary>
    /// <param name="_item">Ячейка снаряжения</param>
    void DragAndDropWeaponSlot(ItemInventory _item)
    {
        if (currentItem.id != _item.id)
        {
            if (_item != null)
                AddInventoryItem(cellCurrentID, _item);
            
            _item.id = currentItem.id;
            _item.count = currentItem.count;
        }
        else
        {
            if (_item.count + currentItem.count <= 128)
            {
                _item.count += currentItem.count;
            }
            else
            {
                AddItem(cellCurrentID, WeaponDataManagerScript.instance.GetElementInventory(_item.id), _item.count + currentItem.count - 128);
                _item.count = 128;
            }

            _item.itemGameObj.GetComponentInChildren<Text>().text = _item.count.ToString();
        }

        if (_item.id != 0)
            _item.itemGameObj.GetComponentInChildren<Text>().text = _item.count.ToString();
        else
            _item.itemGameObj.GetComponentInChildren<Text>().text = "";

        UpdateInventory();
        cellCurrentID = -1;
        movingObject.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class ItemInventory // Ячейка инвентаря
{
    /// <summary>
    /// Номер ячейки
    /// </summary>
    public int id;
    /// <summary>
    /// Ссылка объект на сцене
    /// </summary>
    public GameObject itemGameObj;
    /// <summary>
    /// Количество предметов в ячейке
    /// </summary>
    public int count;
    /// <summary>
    /// Содержимое ячейки
    /// </summary>
    public ElementInventory element;
    /// <summary>
    /// Изображениен предмета
    /// </summary>
    public Sprite standartSprite;
}