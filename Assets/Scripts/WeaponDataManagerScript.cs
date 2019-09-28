using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataManagerScript : MonoBehaviour
{
    public static WeaponDataManagerScript instance;

    // Ячейки для лука и стрел
    public static Transform bowPoint;
    public GameObject bow1;
    public GameObject arrow1;
    public Transform BowPoint;


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

//public class 
