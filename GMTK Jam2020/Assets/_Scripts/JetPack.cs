using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public Rigidbody rb;

    //[SerializeField] private Slider _slider;
    public bool jetPackBroken;

    public float fuel = 70f;
    public float fuelDeplete = 0.5f;
    public float fuelIncreaseSpeed = 0.5f;
    public bool usable = true;

    // Random Jump
    private float randomJumpCooldownTimer;
    private float randomJumpDurationTimer;
    private bool randomJumpReady = false;

    public ParticleSystem randomJumpParticles;
    public ParticleSystem jumpVonk;


    // Update is called once per frame
    void Update()
    {
        //_slider.value = fuel / 100;
        if (!jetPackBroken)
            JetPackFunction();
        else
            JetPackBroken();
    }

    //void JetPackFunction()
    //{
    //    if (fuel > 0 && Input.GetButtonDown("Fire1"))
    //    {
    //        rb.velocity = Vector3.up * gameObject.GetComponent<Player>().jumpVelocity;
    //        gameObject.GetComponent<Player>().randomJumpParticles.Emit(3);
    //        fuel--;
    //    }
    //    else if (fuel <= 0 && fuel <= 100)
    //    {
    //        fuel++;
    //    }
    //}

    public void JetPackFunction()
    {
        if (fuel > fuelDeplete && Input.GetButton("Fire1"))
        {
            randomJumpParticles.Emit(1);
            rb.velocity = Vector3.up * gameObject.GetComponent<Player>().jumpVelocity;
            fuel = fuel - fuelDeplete;
        }
        else if (fuel < 100 && gameObject.GetComponent<Player>().jumpAvailable)
        {
            fuel = fuel + fuelIncreaseSpeed;
        }
    }

    public void JetPackBroken()
    {
        if (!randomJumpReady)
        {
            randomJumpCooldownTimer -= Time.deltaTime;

            if (randomJumpCooldownTimer <= 1f && randomJumpCooldownTimer > 0.8f)
                jumpVonk.Emit(3);

            if (randomJumpCooldownTimer <= 0)
            {
                randomJumpReady = true;
                randomJumpCooldownTimer = Random.Range(2.0f, 4.0f);
            }
        }
        else
        {
            randomJumpDurationTimer -= Time.deltaTime;

            if (randomJumpDurationTimer > 0)
            {
                rb.velocity = Vector3.up * gameObject.GetComponent<Player>().jumpVelocity;
                randomJumpParticles.Emit(3);
            }
            else
            {
                randomJumpDurationTimer = Random.Range(0.25f, 0.5f);
                randomJumpReady = false;
            }
        }
    }
}
