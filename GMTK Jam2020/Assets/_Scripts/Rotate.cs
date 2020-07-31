using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotateX;
    public bool rotateY;
    public bool rotateZ;

    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        if (rotateX)
            transform.Rotate(rotateSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        if (rotateY)
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0.0f, Space.Self);
        if (rotateZ)
            transform.Rotate(0f, 0.0f, rotateSpeed * Time.deltaTime, Space.Self);
    }
}