using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int level; //from 1 to 2
    [SerializeField] private GameObject brickss; // level 1 bricks
    [SerializeField] private GameObject level1_structure;
    [SerializeField] private GameObject brickss2; // level 2 bricks
    [SerializeField] private GameObject level2_structure;
    private GameObject currentBricks;
    private GameObject foundation_brick;
    [SerializeField] private GameObject ball;
    private bool ballActive;
    private int ball_count;
    private bool gameEnd;
    private int max_point_count;
    private int point;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 1;
        ballActive = true;
        gameEnd = false;
        max_point_count = 45; // maximum ball count - level1
        point = 0;
        ball_count = 5;
        currentBricks = Instantiate(brickss, new Vector3(0,0,0), Quaternion.identity);
        foundation_brick = Instantiate(level1_structure, new Vector3(0,0,0), Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(ballActive == false && ball_count > 0 && !(gameEnd)){
                ballActive = true;
                GameObject instance = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
            }
           
            if(gameEnd){
                if(point >= max_point_count){ // Mission Clear
                    print("clear");
                    if(level == 1){
                        level = 2;
                        ballActive = true;
                        gameEnd = false;
                        point = 0;
                        max_point_count = 40;
                        Destroy(currentBricks);
                        Destroy(foundation_brick);
                        currentBricks = Instantiate(brickss2, new Vector3(0,0,0), Quaternion.identity);
                        foundation_brick = Instantiate(level2_structure, new Vector3(0,0,0), Quaternion.identity);
                        GameObject instance = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        ball_count = 5;
                        print("level up");
                    }
                    if (level == 2){
                        print("perfect success");
                    }
                }else{ // Mission Fail
                    print("fail");
                    if(level == 1){
                        ballActive = true;
                        gameEnd = false;
                        point = 0;
                        Destroy(currentBricks);
                        currentBricks = Instantiate(brickss, new Vector3(0,0,0), Quaternion.identity);
                        GameObject instance = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        ball_count = 5;
                    }
                    
                    if(level == 2){
                        ballActive = true;
                        gameEnd = false;
                        point = 0;
                        Destroy(currentBricks);
                        currentBricks = Instantiate(brickss2, new Vector3(0,0,0), Quaternion.identity);
                        GameObject instance = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        ball_count = 5;
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;

        if(point <max_point_count){
            if(ball_count>0){
                GUI.Label(new Rect(0,0,100,200),"Ball: "+ball_count+"",labelStyle);
            }
            else{
                GUI.Label(new Rect(0,0,100,200),"Died",labelStyle);
                GUI.Label(new Rect(Screen.width/2-150,60,100,200),"Game Over",labelStyle);
                GUI.Label(new Rect(Screen.width/2-150,10,100,200),"Press Space Bar To Restart",labelStyle);
                gameEnd = true;
            }

            if(ballActive == false && ball_count>0){
                GUI.Label(new Rect(Screen.width/2-150,40,100,200),"Press Space Bar",labelStyle);
            }
            else {
                GUI.Label(new Rect(Screen.width/2-150,40,100,200),"",labelStyle);
            }   
        }
        else{
            GUI.Label(new Rect(0,0,100,200),"Mission Clear!",labelStyle);
            GUI.Label(new Rect(Screen.width/2-150,60,100,200),"Success",labelStyle);
            if (level == 1){
                GUI.Label(new Rect(Screen.width/2-150,10,100,200),"Press Space Bar For The Next Page",labelStyle);
            }
            else{
                GUI.Label(new Rect(Screen.width/2-150,10,100,200),"Perfect",labelStyle);
            }
            
            gameEnd = true;
        }
       
        GUI.Label(new Rect(0,80,100,200),"Point: "+point+"",labelStyle);
    }

    public void GetPointSignal(){
        point++;
    }

    public void LoseBallSignal(){
        ballActive = false;
        ball_count--;
    }

    public int setterLevel(){
        return level;
    }
}
