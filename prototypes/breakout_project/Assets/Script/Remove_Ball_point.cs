using UnityEngine;

public class Remove_Ball_point : MonoBehaviour
{
    [SerializeField]
    public GameObject ball;
    private bool ballActive;    
    void Start(){
        ballActive = true;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(ballActive == false){
                ballActive = true;
                GameObject instance = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
            }
        }
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("ball")){
            Destroy(other.gameObject);
            ballActive = false;

        }
    }
}
