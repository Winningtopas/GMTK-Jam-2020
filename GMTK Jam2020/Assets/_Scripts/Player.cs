using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float climbingSpeed;
    public float walkSpeed;
    public float zoomSpeed;
    public float sprintSpeed;

    public float currentSpeed;

    public Vector3 playerPosition;

    [SerializeField]
    public GameObject PlayerModel;
    [SerializeField]
    public GameObject Hand;
    [SerializeField]
    public GameObject AttackParticle;

    Rigidbody rb;

    //Camera
    //public Camera CameraZoom;
    //public Camera CameraUnzoomed;

    //public GameObject CameraZoom;
    //public GameObject CameraUnzoomed;
    public bool zoom;

    // jump
    private float fJumpPressedRemember;
    private float fJumpPressedRememberTime = 0.25f;

    private float fGroundRemember;
    private float fGroundRememberTime = 0.1f;

    public bool jumpAvailable;
    public float jumpVelocity;

    public float distanceGround;
    public bool isGrounded = false;

    // Random Jump
    private float randomJumpCooldownTimer;
    private float randomJumpDurationTimer;
    private bool randomJumpReady = false;

    //brackeys move

    public CharacterController controller;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothvelocity;

    public ParticleSystem randomJumpParticles;

    public Vector3 gravity = new Vector3(0, -2.0F, 0);

    public Vector3 lastCheckPointPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;

        randomJumpCooldownTimer = Random.Range(2.0f, 4.0f);
        randomJumpDurationTimer = Random.Range(0.25f, 0.5f);

        //Physics.gravity = gravity;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        newMove();
    }

    void Update()
    {

        //RandomJump();

        playerPosition = this.transform.position;
        Attack();
        Speed();
        //ActiveCamera();
        jumpAvailable = IsGrounded();
    }

    void newMove()
    {

    }

    void HandleMovement()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        direction.Normalize();

        transform.Translate(direction * Time.deltaTime * currentSpeed);
        this.GetComponent<Rigidbody>().useGravity = true;

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpAvailable == true)
                rb.velocity = Vector3.up * jumpVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            this.gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "MovingPlatform")
            this.gameObject.transform.parent = null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "CheckPoint")
        {
            lastCheckPointPosition = transform.position;
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Emit(30);
        }
        if (collision.tag == "Death")
        {
            //play death sound
            //fade to black
            //respawn particles
            rb.isKinematic = true;
            rb.isKinematic = false;
            transform.position = lastCheckPointPosition;
        }
    }

    void RandomJump()
    {
        if (!randomJumpReady)
        {
            randomJumpCooldownTimer -= Time.deltaTime;
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
                rb.velocity = Vector3.up * jumpVelocity;
                randomJumpParticles.Emit(3);
            }
            else
            {
                randomJumpDurationTimer = Random.Range(0.25f, 0.5f);
                randomJumpReady = false;
            }
            //jump for x seconds
            //
            //emit particles
        }
    }

    //void ActiveCamera()
    //{
    //    if (Input.GetButton("Fire2"))
    //    {
    //        zoom = true;
    //        CameraZoom.gameObject.SetActive(true);
    //        CameraUnzoomed.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        zoom = false;
    //        CameraZoom.gameObject.SetActive(false);
    //        CameraUnzoomed.gameObject.SetActive(true);
    //    }
    //}

    void Speed()
    {
        currentSpeed = climbingSpeed;

        if (zoom == true)
        {
            currentSpeed = zoomSpeed;
        }
        else
        {
            if (Input.GetButton("Sprint") && jumpAvailable)
                currentSpeed = sprintSpeed;
            else
                currentSpeed = walkSpeed;
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && zoom == true)
        {
            GameObject attackParticle = Instantiate((AttackParticle), Hand.gameObject.transform.position, Hand.gameObject.transform.rotation);
            attackParticle.name = "AttackParticle";
        }
    }

    private bool IsGrounded()
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f))
            isGrounded = false;
        else
            isGrounded = true;
        return (isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.y * distanceGround);
    }
}