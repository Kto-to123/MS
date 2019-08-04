using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public GameObject Player;
    public bool NormalTime = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            NormalTime = !NormalTime;
        }

        if (NormalTime)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0.01f;
        }
    }
}
