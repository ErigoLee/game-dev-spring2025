using UnityEngine;

public class Remove_Ball_point : MonoBehaviour
{
    [SerializeField]
    public GameObject ball;
    private bool ballActive;
    private int ball_count = 5;
    private int point = 0;    
    void Start(){
        ballActive = true;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(ballActive == false && ball_count > 0){
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
            ball_count--;
        }
    }
    
    void OnGUI(){
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 32;
        if(ball_count>0){
            GUI.Label(new Rect(0,0,100,200),"Ball: "+ball_count+"",labelStyle);
        }
        else{
            GUI.Label(new Rect(0,0,100,200),"Died",labelStyle);
        }

        if(ballActive == false && ball_count>0){
            GUI.Label(new Rect(0,40,100,200),"Press Space Bar",labelStyle);
        }
        else {
            GUI.Label(new Rect(0,40,100,200),"");
        }

        GUI.Label(new Rect(0,80,100,200),"Point: "+point+"",labelStyle);


    }

    public void ReceiveSignal(){
        point++;
    }
}
