using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private int level; //from 1 to 2
    [SerializeField] private GameObject brickss; // level 1 bricks
    [SerializeField] private GameObject level1_structure;
    [SerializeField] private GameObject brickss2; // level 2 bricks
    [SerializeField] private GameObject level2_structure;
    [SerializeField] private GameObject initialPanel;
    private GameObject currentBricks;
    private GameObject foundation_brick;
    [SerializeField] private GameObject ball;
    private GameObject ball_obj;
    private bool ballActive;
    private int ball_count;
    private bool gameEnd;
    private int max_point_count;
    private int point;
    //page
    private int page = 1;
    private int page_min = 1;
    private int page_max = 3;
    
    public GUISkin skin;
    [SerializeField] private GameObject cam;

    float timeBetweenInputs = 0.2f;
    float lastInputTime = 0.0f;
    bool checkingTime = false;
    void Start()
    {


        level = 0;
        gameEnd = true; // not start!


        //level = 1;
        //ballActive = true;
        //gameEnd = false;
        //max_point_count = 45; // maximum ball count - level1
        //point = 0;
        //ball_count = 5;
        //currentBricks = Instantiate(brickss, new Vector3(0,0,0), Quaternion.identity);
        //foundation_brick = Instantiate(level1_structure, new Vector3(0,0,0), Quaternion.identity);
    }
    
    void initialPage(){
        Destroy(currentBricks);
        Destroy(foundation_brick);
        if(ball_obj != null)
            Destroy(ball_obj);
        initialPanel.SetActive(true);
        level = 0;
        page = 1;
        cam.transform.position = new Vector3(0f, 27f, -36.7f);
        cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        if(level == 0){
            pageUpDown();
            if(Input.GetKeyDown(KeyCode.Space)){
                initialPanel.SetActive(false);
                cam.transform.position = new Vector3(0f, 1f, -36.7f);
                cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                switch(page){
                    case 1:
                        print("level1");
                        level = 1;
                        ballActive = true;
                        ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        gameEnd = false;
                        max_point_count = 45;
                        point = 0;
                        ball_count = 5;
                        currentBricks = Instantiate(brickss, new Vector3(0,0,0), Quaternion.identity);
                        foundation_brick = Instantiate(level1_structure, new Vector3(0,0,0), Quaternion.identity);
                    break;
                    case 2:
                        level = 2;
                        ballActive = true;
                        ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        gameEnd = false;
                        max_point_count = 40;
                        point = 0;
                        ball_count = 5;
                        currentBricks = Instantiate(brickss2, new Vector3(0,0,0), Quaternion.identity);
                        foundation_brick = Instantiate(level2_structure, new Vector3(0,0,0), Quaternion.identity);
                    break;
                    case 3:
                        level = 3;
                        ballActive = true;
                        ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                        gameEnd = false;
                        max_point_count = 40;
                        point = 0;
                        ball_count = 5;
                        currentBricks = Instantiate(brickss2, new Vector3(0,0,0), Quaternion.identity);
                        foundation_brick = Instantiate(level2_structure, new Vector3(0,0,0), Quaternion.identity);
                    break;
                }

            }
        }
        else{
            if(point>=max_point_count || ball_count == 0)
                gameEnd = true;

            if(Input.GetKeyDown(KeyCode.Space)){
                if(ballActive == false && ball_count > 0 && !(gameEnd)){
                    ballActive = true;
                    ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                }
           
                if(gameEnd){
                    print("gameEnd");
                    if(point >= max_point_count){ // Mission Clear
                        initialPage();
                        
                    }else{ // Mission Fail
                        print("fail");
                        if(level == 1){
                            ballActive = true;
                            gameEnd = false;
                            point = 0;
                            Destroy(currentBricks);
                            if(ball_obj != null)
                              Destroy(ball_obj);
                            currentBricks = Instantiate(brickss, new Vector3(0,0,0), Quaternion.identity);
                            ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                            ball_count = 5;
                        }
                    
                        if(level == 2 || level == 3){
                            ballActive = true;
                            gameEnd = false;
                            point = 0;
                            Destroy(currentBricks);
                            if(ball_obj != null)
                              Destroy(ball_obj);
                            currentBricks = Instantiate(brickss2, new Vector3(0,0,0), Quaternion.identity);
                            ball_obj = Instantiate(ball, new Vector3(0,-2.61f,0),Quaternion.identity);
                            ball_count = 5;
                        }
                    }
                }
            }
        


        
        }
    }

    void pageUpDown(){
        if(checkingTime == false){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                page--;
                if(page > page_max)
                    page = page_min;
                checkingTime = true;
                lastInputTime = 0.0f;  // 입력이 있을 때마다 시간 초기화
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                page++;
                if(page < page_min)
                    page = page_max;
                checkingTime = true;
                lastInputTime = 0.0f;  // 입력이 있을 때마다 시간 초기화
            }
        }
        else{
            if(lastInputTime >= timeBetweenInputs){
                checkingTime = false; // 시간이 지나면 입력을 받을 수 있도록 설정
            }
            else{
                lastInputTime += Time.deltaTime;  // 매 프레임마다 시간 증가
            }
        }
    }
    

    void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;

        if(level == 0){
            int screenwidth = Screen.width;
            int screenheight = Screen.height;
            switch(page){
                case 1:
                    GUI.Label(new Rect(0, 3 * screenheight/10, screenwidth,screenheight/4),"Level1","Selected");
                    GUI.Label(new Rect(0, 5 * screenheight/10, screenwidth,screenheight/4),"Level2","UnSelected");
                    GUI.Label(new Rect(0, 7 * screenheight/10, screenwidth,screenheight/4),"Level3","UnSelected");
                    break;
                case 2:
                    GUI.Label(new Rect(0, 3 * screenheight/10, screenwidth,screenheight/4),"Level1","UnSelected");
                    GUI.Label(new Rect(0, 5 * screenheight/10, screenwidth,screenheight/4),"Level2","Selected");
                    GUI.Label(new Rect(0, 7 * screenheight/10, screenwidth,screenheight/4),"Level3","UnSelected");
                    break;
                case 3:
                    GUI.Label(new Rect(0, 3 * screenheight/10, screenwidth,screenheight/4),"Level1","UnSelected");
                    GUI.Label(new Rect(0, 5 * screenheight/10, screenwidth,screenheight/4),"Level2","UnSelected");
                    GUI.Label(new Rect(0, 7 * screenheight/10, screenwidth,screenheight/4),"Level3","Selected");
                    break;
            }
        }
        else{
            if(point <max_point_count){
                if(ball_count>0){
                    GUI.Label(new Rect(0,0,100,200),"Ball: "+ball_count+"",labelStyle);
                }
                else{
                    GUI.Label(new Rect(0,0,100,200),"Died",labelStyle);
                    GUI.Label(new Rect(Screen.width/2-150,60,100,200),"Game Over",labelStyle);
                    GUI.Label(new Rect(Screen.width/2-150,10,100,200),"Press Space Bar To Restart",labelStyle);
                }

                if(ballActive == false && ball_count>0){
                    GUI.Label(new Rect(Screen.width/2-150,40,100,200),"Press Space Bar",labelStyle);
                }
                else {
                    GUI.Label(new Rect(Screen.width/2-150,40,100,200),"",labelStyle);
                }   
            }
            else {
                GUI.Label(new Rect(0,0,100,200),"Mission Clear!",labelStyle);
                GUI.Label(new Rect(Screen.width/2-150,60,100,200),"Success",labelStyle);
                GUI.Label(new Rect(Screen.width/2-150,10,100,200),"Perfect",labelStyle);
            }
            GUI.Label(new Rect(0,80,100,200),"Point: "+point+"",labelStyle);
        }

        
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
