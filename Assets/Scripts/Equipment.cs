using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Sprite image;
    public EquipmanType type;
    public int armor;
}

public enum EquipmanType
{
    Shlem,
    Dospeh,
    Perchatki,
    Nalakotniki,
    Poyas,
    Shtani,
    Nakalenniki,
    Obyv
}
