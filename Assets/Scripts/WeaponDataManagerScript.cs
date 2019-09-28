using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataManagerScript : MonoBehaviour
{
    public static WeaponDataManagerScript instance;

    [SerializeField]
    List<Element> Elements = new List<Element>();

    // Ячейки для лука и стрел
    public Transform bowPoint;
    public GameObject bow1;
    public GameObject DropPrefab1;
    public GameObject arrow1;
    public Transform BowPoint;

    public GameObject GetWeapon(int _ID)
    {
        bow1.GetComponent<WeaponScript>().ID = _ID;
        return bow1;
    }

    public GameObject GetDropPrefab(int id)
    {
        return DropPrefab1;
    }

    public static GameObject Inst(GameObject _gameObject, Transform _transform)
    {
        return Instantiate(_gameObject, _transform);
    }

    public static GameObject Inst(GameObject _gameObject, Vector3 _Vector3, Quaternion _Quaternion)
    {
        return Instantiate(_gameObject, _Vector3, _Quaternion);
    }

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
}

[System.Serializable]
public struct Element
{
    public int id;
    public Transform bowPoint;
    public GameObject bow1;
    public GameObject DropPrefab1;
    public GameObject arrow1;
    public Transform BowPoint;
}
