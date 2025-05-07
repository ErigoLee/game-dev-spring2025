using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    //Camera
    [SerializeField]
    GameObject cam;
    [SerializeField] private Transform camPivot; // camera pivot
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private float pitchMin = -30f;
    [SerializeField] private float pitchMax = 60f;
    
    [SerializeField] private float zoomDistance = 5f;           // Zoom distance
    [SerializeField] private float zoomMin = 2f, zoomMax = 10f; // Zoom limits
    [SerializeField] private float zoomSpeed = 2f;              // Zoom speed

    //Animator
    [SerializeField] private Animator animator;

    //Character
    CharacterController cc;
    Vector3 velocity = Vector3.zero;
    float yVelocity = 0;
    float moveSpeed = 12;
    float jumpForce = 10;  // Increased jump force for better jump height
    float gravity = -19.81f;
    
    private float pitch = 0f;
    bool doublejump = false;
    
    [SerializeField]
    private GameManager gameManager;
    private Reward reward;

    //private float maxRightAngle = 20.0f;
    //private float maxLeftAngle = -20.0f;
    //private float rotSpeed = 0.0f;

    [SerializeField]
    private GameObject bullet;
    private bool bulletTimer = false;
    private float shootTime = 1.0f;
    private float timer = 0.0f;
    [SerializeField]
    private GameObject bulletSpawnPoint;


    //level2 
    private int bullets;

    //soundManager
    [SerializeField] private SoundManager soundManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        bulletTimer = false;
        bullets = gameManager.GettingBullets();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input for movement
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        // Reset velocity, note we are using a separate float for the yVelocity that 
        // IS NOT reset, and in fact is added to this velocity vector's y value every
        // frame.
        velocity = Vector3.zero;

        // Get the camera's right direction for horizontal movement
        Vector3 adjustedCamRight = cam.transform.right;
        adjustedCamRight.y = 0; // Ignore vertical tilt of the camera
        adjustedCamRight.Normalize();
        velocity += adjustedCamRight * hAxis * moveSpeed;

        // Get the camera's forward direction for vertical movement
        Vector3 adjustedCamForward = cam.transform.forward;
        adjustedCamForward.y = 0; // Ignore vertical tilt of the camera
        adjustedCamForward.Normalize();
        velocity += adjustedCamForward * vAxis * moveSpeed;

        // Animator: Walk 
        bool isMoving = hAxis != 0 || vAxis != 0;
        animator.SetBool("walk", isMoving);
       

        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(0,mouseX * mouseSensitivity, 0);

            pitch -= mouseY * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
            camPivot.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        // Zoom in/out with mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            zoomDistance -= scroll * zoomSpeed;
            zoomDistance = Mathf.Clamp(zoomDistance, zoomMin, zoomMax);
        }

        /*
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            rotSpeed = maxLeftAngle;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            rotSpeed = maxRightAngle;
        }
        else{
            rotSpeed = 0.0f;
        }
        
        
        transform.Rotate(0,rotSpeed * Time.deltaTime,0);
        */
        // Gravity and Jumping
        if (cc.isGrounded)
        {
            // Reset yVelocity when on the ground to a small value so isGrounded is true 
            // for sure whenever we are near on the ground. (i.e. even after cc.Move prevented
            // us from going into the ground.
            yVelocity = -2; 

            // Check for jump input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce; // Apply jump force
                if(soundManager!=null){
                    soundManager.JumpEffect();
                }
            }
            doublejump = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && !(doublejump)){
                doublejump = true;
                yVelocity = jumpForce; // Apply jump force
            }

            // Apply gravity when in the air
            yVelocity += gravity * Time.deltaTime;
        }

        // Apply vertical velocity to movement
        velocity.y = yVelocity;


        Vector3 nextPostion = transform.position + velocity * Time.deltaTime;

        int level = gameManager.GettingLevel();

        switch (level){
            case 0: // Temporary
                if (nextPostion.x < -59f || nextPostion.x > 59f || nextPostion.z < -50f || nextPostion.z >58f){
                    velocity = Vector3.zero;
                }
            break;
            case 1:
                if (nextPostion.x < -39f || nextPostion.x > 39f || nextPostion.z < -30 || nextPostion.z >38.8){
                    velocity = Vector3.zero;
                } 
            break;
            case 2:
                if (nextPostion.x < -59f || nextPostion.x > 59f || nextPostion.z < -50f || nextPostion.z >58f){
                    velocity = Vector3.zero;
                } 
            break;
        }

        


        // Limit movement speed
        velocity = Vector3.ClampMagnitude(velocity, 10);

        // Move the character
        cc.Move(velocity * Time.deltaTime);


        if(bulletTimer){
            if(timer<shootTime){
                timer = timer + Time.deltaTime;
            }
            else{
                timer = 0.0f;
                bulletTimer = false;
            }
        }
        else{
            if (Input.GetKey(KeyCode.X))
            {
                animator.SetBool("attack", true); // Start attack animation
                if(soundManager!=null){
                    soundManager.ShootBulletEffect();
                }
                
                if(level == 0 || level == 2){
                    bullets = gameManager.GettingBullets();
                    if(bullets > 0){
                        bullets--;
                        Instantiate(bullet,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                        bulletTimer = true;
                        gameManager.ReducingBullets();
                    }
                    
                }
                else{
                    Instantiate(bullet,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
                    bulletTimer = true;
                }
                
            }
            else{
                animator.SetBool("attack", false); // Reset attack animation
            }
        }

        // Update camera position and rotation
        camPivot.position = transform.position + new Vector3(0, 1.5f, 0);
        cam.transform.position = camPivot.position - camPivot.forward * zoomDistance + camPivot.up * 1f;
        cam.transform.LookAt(camPivot);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Reward")){
            gameManager.gettingPoint();
            if(soundManager!=null){
                soundManager.RewardEffect();
            }
            reward = other.GetComponent<Reward>();
            reward.regwardDestory();
        }

        if(other.CompareTag("LowWall")){
            gameManager.CheckingLowWall();
        }

        if (other.CompareTag("Portal")){
            gameManager.ChangeLevel();
        }

        if (other.CompareTag("Key")){
            gameManager.ChangeLevel();
        }
    }
    

}
