using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public GameObject Player;

    private void Start()
    {
        
    }

    private void Update()
    {

        if (Input.anyKey)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.01f;
        }
    }
}
