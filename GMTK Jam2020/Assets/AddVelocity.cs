using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVelocity : MonoBehaviour
{

    [SerializeField]
    Vector3 v3Force;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity += v3Force;
    }
}
