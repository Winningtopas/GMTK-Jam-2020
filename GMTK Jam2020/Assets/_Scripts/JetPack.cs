using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public Rigidbody rb;

    //[SerializeField] private Slider _slider;
    public bool jetPackBroken;

    public float fuelMax = 5f;
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

    public AudioSource heenWeg;
    public AudioSource terugWeg;
    public AudioSource jetPackNoises;

    private bool doOnce = true;
    public float brokenJumpIncrease = 2f;
    public GameObject boxMessage;

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

        //if (fuel > 0 && Input.GetButton("Fire1"))
        //    {
        //        jetPackNoises.Play();


        //        randomJumpParticles.Emit(1);
        //        rb.velocity = Vector3.up * gameObject.GetComponent<Player>().jumpVelocity;
        //        fuel = fuel - fuelDeplete * Time.deltaTime;
        //    }
        //    else if (fuel < 100 && gameObject.GetComponent<Player>().jumpAvailable)
        //    {
        //        fuel = fuel + fuelIncreaseSpeed * Time.deltaTime;
        //    }
        //}
        if (fuel > fuelDeplete && Input.GetButtonDown("Fire2"))
        {
            jetPackNoises.Play();
            randomJumpParticles.Emit(20);
            rb.velocity = Vector3.up * gameObject.GetComponent<Player>().jumpVelocity;
            fuel = fuel - fuelDeplete;
        }
        else if (fuel < fuelMax && gameObject.GetComponent<Player>().jumpAvailable)
        {
            fuel = fuel + fuelIncreaseSpeed * Time.deltaTime;
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
                if (doOnce)
                {
                    jetPackNoises.Play();
                    doOnce = false;
                }
                rb.velocity = Vector3.up * (gameObject.GetComponent<Player>().jumpVelocity + brokenJumpIncrease);
                randomJumpParticles.Emit(3);
            }
            else
            {
                doOnce = true;
                randomJumpDurationTimer = Random.Range(0.25f, 0.5f);
                randomJumpReady = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JetPackBreak")
        {
            jetPackBroken = true;
            heenWeg.Stop();
            terugWeg.Play();
            Destroy(other.gameObject);
            boxMessage.SetActive(true);
        }
    }
}
