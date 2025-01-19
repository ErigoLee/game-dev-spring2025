using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rb;
    public Material redMaterial;
    public Material orangeMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    private Color red;
    private Color orange;
    private Color yellow;
    private Color green;
    private Color blue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1){
            rb.linearVelocity = new Vector3(-5, -10, 0);
        }
        else{
            rb.linearVelocity = new Vector3(5, -10, 0);
        }
        red = redMaterial.color;
        orange = orangeMaterial.color;
        yellow = yellowMaterial.color;
        green = greenMaterial.color;
        blue = blueMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {        

    }

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
            Destroy(collision.gameObject);

        }
        if(collision.gameObject.CompareTag("wall")){
            rb.linearVelocity = new Vector3(5,-10,0);
        }
    }
}
