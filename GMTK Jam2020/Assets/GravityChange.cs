using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{

    public Vector3 gravityNormal = new Vector3(0, -9.81F, 0);
    public Vector3 gravityReverse = new Vector3(0, 5.0F, 0);
    public bool gravityReverseBool;
    public float cooldown = 5f;
    public float timeLeft;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {

            if (gravityReverseBool)
            {
                Physics.gravity = gravityNormal;
                gravityReverseBool = false;
            }
            else
            {
                Physics.gravity = gravityReverse;
                gravityReverseBool = true;
            }
            timeLeft = cooldown;
        }
    }
}
