using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    public Transform head;

    public float sensitivity = 3f; // чувствительность мыши
    public float headMinY = -40f; // ограничение угла для головы
    public float headMaxY = 40f;

    private Vector3 direction;
    private float h, v;
    private int layerMask;
    private Rigidbody body;
    private float rotationY;
    private float rotationX;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        layerMask = 1 << gameObject.layer | 1 << 2;
        layerMask = ~layerMask;
    }


    void Update()
    {
        if (!Cursor.visible)
        {
            rotationX += /*head.localEulerAngles.y +*/ Input.GetAxis("Mouse X") * sensitivity;
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            if (rotationY > headMaxY)
            {
                rotationY = headMaxY;
            }
            if (rotationY < headMinY)
            {
                rotationY = headMinY;
            }
            //rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
            head.localEulerAngles = new Vector3(-rotationY, 0, 0);

            transform.localEulerAngles = new Vector3(0, rotationX, 0);
        }
    }
}
