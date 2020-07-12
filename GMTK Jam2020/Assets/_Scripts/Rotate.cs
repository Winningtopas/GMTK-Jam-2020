using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool rotate;
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(Vector3.zero, Vector3.up, 2 * Time.deltaTime);
        if (rotate)
            transform.Rotate(0f, rotateSpeed, 0.0f, Space.Self);
        //transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), 30 * Time.deltaTime);
    }
}