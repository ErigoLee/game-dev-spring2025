using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float jumpForce = 8f;
    public float gravity = -20f;
    public float maxLeftAngle = -90f;
    public float maxRightAngle = 90f;

    public Camera cam;  // Main Camera 연결 필요

    private CharacterController cc;
    private Animator animator;

    private Vector3 velocity;
    private float yVelocity;
    private float rotSpeed;
    private bool doublejump;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get keyboard input
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Calculate move direction based on camera
        Vector3 moveDirection = Vector3.zero;
        Vector3 adjustedCamRight = cam.transform.right;
        adjustedCamRight.y = 0;
        adjustedCamRight.Normalize();

        Vector3 adjustedCamForward = cam.transform.forward;
        adjustedCamForward.y = 0;
        adjustedCamForward.Normalize();

        moveDirection += adjustedCamRight * hAxis;
        moveDirection += adjustedCamForward * vAxis;
        moveDirection.Normalize();

        // Animator walking state
        bool isWalking = moveDirection.magnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);

        // Apply movement
        velocity = moveDirection * moveSpeed;

        // Handle rotation based on A/D or Left/Right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotSpeed = maxLeftAngle;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotSpeed = maxRightAngle;
        }
        else
        {
            rotSpeed = 0f;
        }
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);

        // Gravity and jumping
        if (cc.isGrounded)
        {
            yVelocity = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce;
            }

            doublejump = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !doublejump)
            {
                yVelocity = jumpForce;
                doublejump = true;
            }

            yVelocity += gravity * Time.deltaTime;
        }

        // Apply vertical movement
        velocity.y = yVelocity;

        Vector3 nextPosition = transform.position + velocity * Time.deltaTime;

        if(nextPosition.x < -38f || nextPosition.x > 38f || nextPosition.z < -35f || nextPosition.z > 35f)
        {
            velocity = Vector3.zero;
        }

        // Move character
        cc.Move(velocity * Time.deltaTime);
    }
}
