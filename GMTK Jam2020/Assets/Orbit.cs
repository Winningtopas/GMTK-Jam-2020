using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;
    public float y;
    public float z;


    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + y, player.position.z + z);
    }
    private void Update()
    {

        transform.rotation = new Quaternion(0f, transform.rotation.y, transform.rotation.z, 1f);
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
