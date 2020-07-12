using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakJetPack : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<JetPack>() != null && collision.gameObject.GetComponent<JetPack>().jetPackBroken == false)
            collision.gameObject.GetComponent<JetPack>().jetPackBroken = true;
    }
}
