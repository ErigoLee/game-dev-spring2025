using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rb;
    [SerializeField]
    Camera cam;
    //Material variables
    public Material redMaterial;
    public Material orangeMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    //Color variables
    private Color red;
    private Color orange;
    private Color yellow;
    private Color green;
    private Color blue;
    //GameObject variables
    private GameObject gameManagerObj;
    private GameManager gameManager;
    //Particle Object variables
    public GameObject splatterParticles;
    //base gameObject
    private GameObject baseObject;
    //sound part
    private GameObject soundObject;
    private Sound_Manager sound_Manager;
    
    private int level; 

    //private Vector3 previousMouse;
    //private Vector3 previousMouse2;

    void Start()
    {
        //The position of ball is reflected by the position of base
        baseObject = GameObject.Find("Base");
        float y = transform.position.y;
        transform.position = new Vector3(baseObject.transform.position.x, y,baseObject.transform.position.z);  
        //previousMouse = Vector3.zero;
        //previousMouse2 = Vector3.zero;
        //checking remove ball point object
        gameManagerObj = GameObject.Find("Game_Manager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1){
            rb.linearVelocity = new Vector3(-5, -10, 0);
        }
        else{
            rb.linearVelocity = new Vector3(5, -10, 0);
        }

        //color
        red = redMaterial.color;
        orange = orangeMaterial.color;
        yellow = yellowMaterial.color;
        green = greenMaterial.color;
        blue = blueMaterial.color;

        //sound
        soundObject = GameObject.Find("Sound_Manager");
        sound_Manager = soundObject.GetComponent<Sound_Manager>();
        level = gameManager.setterLevel();
    }
    void Update()
    {
        level = gameManager.setterLevel();
        if (level == 2){
            if(Input.GetMouseButton(0)){
                Vector3 ballInScreenCoordinateSpace = cam.WorldToScreenPoint(transform.position);
                //previousMouse = Input.mousePosition;
                //previousMouse2 = ballInScreenCoordinateSpace;
                Vector3 directionToMouse = Input.mousePosition - ballInScreenCoordinateSpace;
                directionToMouse.z = 0;
                directionToMouse = directionToMouse.normalized;
                rb.AddForce(directionToMouse*100f);
                //baseObject.transform.position = new Vector3(0f, -8f, 0f);
            }
        }

        if(level == 3){
            if(Input.GetKey(KeyCode.W)){
                print("W");
                Vector3 up = Vector3.up;
                rb.AddForce(up*2);
            }
            if(Input.GetKey(KeyCode.S)){
                Vector3 down = Vector3.down;
                rb.AddForce(down*2);
            }
            if(Input.GetKey(KeyCode.D)){
                Vector3 right = Vector3.right;
                rb.AddForce(right*2);
            }
            if(Input.GetKey(KeyCode.A)){
                Vector3 left = Vector3.left;
                rb.AddForce(left*2);
            }
        }
        
    }
    /*
    void OnGUI(){
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;
        string name = previousMouse.ToString();
        string name2 = previousMouse2.ToString();
        GUI.Label(new Rect(0,130,100,200),name,labelStyle);
        GUI.Label(new Rect(0,170,100,200),name2,labelStyle);
    }
    */
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("brick")){
            Color color = collision.gameObject.GetComponent<Renderer>().material.color;
            if(color == red)
            {
                rb.linearVelocity = new Vector3(-10,20,0);
                print("red");

            }
            else if(color == orange){
                rb.linearVelocity = new Vector3(9,-18,0);
                print("orange");
            }
            else if (color == yellow){
                rb.linearVelocity = new Vector3(-8,16,0);
                print("yellow");
            }
            else if(color == green){
                rb.linearVelocity = new Vector3(7,-14,0);
                print("green");
            }
            else{
                rb.linearVelocity = new Vector3(-6,12,0);
                print("blue");
            }
            
            if (gameManager != null){
                gameManager.GetPointSignal();
            }
            ContactPoint contact = collision.contacts[0];
            GameObject particle = Instantiate(splatterParticles, contact.point, Quaternion.identity);
            Destroy(particle, 1.0f);
            Destroy(collision.gameObject);
            sound_Manager.explodeEffect();
        }
        else{
            sound_Manager.throwEffect();
        }
        if(collision.gameObject.CompareTag("wall")){
            rb.linearVelocity = new Vector3(5,-10,0);
            sound_Manager.throwEffect();
        }
    }
}
