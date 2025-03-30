using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GUISkin skin;


    private int point;
    private int health;

    private int targetPoint;

    //level
    private int level;
    private int level1;

    //player
    [SerializeField] private GameObject player;
    private PlatformerPlayerController platformerPlayerController;
    private Transform initalPTransform;

    //Begin - level 0
    [SerializeField] private Button tutoBut;
    [SerializeField] private Button playBut;
    [SerializeField] private GameObject begin;

    //Tutorial - level 1
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private GameObject lowWall;
    
    private bool checkingWall;
    //Targets
    [SerializeField] private GameObject targets;
    private GameObject targets_instance;
    private int target_num;
    private int max_target_num;
    private bool target_num_reached;
    
    //Enemies
    [SerializeField] private GameObject enemies_level1;
    private GameObject enemies_level1_instance;
    private int enemies_num_level1;
    private int max_enemies_num_level1;
    private bool enemies_level1_reached;


    //point_num
    private int point_level1_num;
    private int point_level1_num_max;

    //Portal
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject textObj;



    //Play - level 2
    private Vector3 initPos;
    private int bullets;
    private int enemies_num_level2;
    private int max_enemies_num_level2;
    private bool enemies_level2_alldie;
    [SerializeField] private GameObject enemyObj2;
    [SerializeField] private GameObject keyObj2;
    [SerializeField] private GameObject playObj;
    private List<GameObject> coins;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button gotoBeginBut;
    private GameObject acutal_enemyObj2;

    //Timer
    private float eslapedTime;
    private float timerInterval;
    private bool acceptedTime;

    private TMP_Text text;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        point = 0;
        health = 100;
        targetPoint = 0;
        level = 0;
        level1 = 0;
        timerInterval = 1.0f;
        eslapedTime = 0.0f;
        acceptedTime = true;
        
        //script inactive
        platformerPlayerController = player.GetComponent<PlatformerPlayerController>();
        platformerPlayerController.enabled = false;

        //level0
        tutoBut.onClick.AddListener(OnButtonClickTuto);
        playBut.onClick.AddListener(OnButtonClickPlay);

        //level1
        text = textObj.GetComponent<TMP_Text>();
        checkingWall = false;
        initalPTransform = player.transform;
        //Target
        target_num = 0;
        max_target_num = 6;
        target_num_reached = true;
        //Enemies
        enemies_num_level1 = 0;
        max_enemies_num_level1 = 7;
        enemies_level1_reached = true;
        //coin
        point_level1_num = 0;
        point_level1_num_max = 7;


        //level2
        initPos = new Vector3(-43.2f, 6f, -43.5f);
        bullets = 10;
        enemies_num_level2 = 0;
        max_enemies_num_level2 = 4;
        enemies_level2_alldie = false;
        coins = new List<GameObject>();
        gotoBeginBut.onClick.AddListener(onButtonClickBegin);
    }

    // Update is called once per frame
    void Update()
    {
        switch (level){
            case 1:
                if(Input.GetKeyDown(KeyCode.T)){
                    if(!checkingWall && target_num_reached && enemies_level1_reached){
                        ChangeLevel1();
                        level1++;
                    }
                }
            break;
            case 2:
                if(enemies_level2_alldie)
                    keyObj2.SetActive(true);
            break;
        }
    }


    public void ChangeLevel1(){
        if(textObj != null){
            
            if(level==1){
                switch(level1){
                    case 0:
                        text.text = "You can jump \nif you press the spacebar.";
                    break;
                    case 1:
                        text.text = "You can double jump \nif you press the spacebar twice.";
                    break;
                    case 2:
                        text.text = "You can go forward \nif you press the 'W' key \nor the up arrow key.";
                    break;
                    case 3:
                        text.text = "You can go backward \nif you press the 'S' key \nor the down arrow key.";
                    break;
                    case 4:
                        text.text = "You can go left \nif you press the 'A' key \nor the left arrow key.";
                    break;
                    case 5:
                        text.text = "You can go right \nif you press the 'D' key \nor the right arrow key.";
                    break;
                    case 6:
                        lowWall.SetActive(true);
                        text.text = "You can climb the wall \nif you press the spacebar twice.";
                        checkingWall = true;
                    break;
                    case 7:
                        targets_instance = Instantiate(targets, new Vector3(-10f,0f,-30f), Quaternion.Euler(0f,-90f,0f));
                        text.text = "You can shoot the target \nif you press the 'X' key";
                        target_num_reached = false;
                    break;
                    case 8:
                        Destroy(targets_instance);
                        enemies_level1_instance = Instantiate(enemies_level1,new Vector3(0f,0f,0f),Quaternion.Euler(0f,0,0f));
                        enemies_level1_reached = false;
                        text.text = "You can get coin \nafter you kill the enemies.";
                    break;
                    case 9:
                        Destroy(enemies_level1_instance);
                        text.text = "You can end the section! \n Let's go to the next section!";
                        portal.SetActive(true);
                    break;
                    
                }
            }
           
        }
    }


    public void ChangeLevel(){
        switch (level){
            case 1:
                portal.SetActive(false);
                tutorialObj.SetActive(false);
                begin.SetActive(true);
                level=0;
                level1=0;
                point=0;
                targetPoint = 0;
                target_num = 0;
                point_level1_num = 0;
                enemies_num_level1 = 0;
                text.text = "<Tutorial>\nPress 'T'\nif you can go to the next page.";
            break;
            case 2:
                keyObj2.SetActive(false);
                playObj.SetActive(false);
                begin.SetActive(true);
                level = 0;
                health = 100;
                bullets = 10;
                enemies_level2_alldie = false;
                enemies_num_level2 = 0;
                int size = coins.Count;
                for(int i=0;i<size;i++){
                    if(coins[i] != null){
                    Destroy(coins[i]);
                    }
                }
                coins.Clear();
                Destroy(acutal_enemyObj2);
            break;
        }
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; 
            controller.transform.position = new Vector3(0f, 0f, -20f); 
            controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            controller.enabled = true; 
        }
        else
        {
            Debug.LogError("There is no CharacterController");
        }
    }

    public void gettingPoint(){
        point = point + 10;
        switch (level){
            case 1:
                point_level1_num++;
                if(point_level1_num>=point_level1_num_max){
                    enemies_level1_reached = true;
                    CharacterController controller = player.GetComponent<CharacterController>();
                    if (controller != null)
                    {
                        controller.enabled = false; 
                        controller.transform.position = new Vector3(0f, 0f, -20f); 
                        controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                        controller.enabled = true; 
                    }
                    else
                    {
                        Debug.LogError("There is no CharacterController");
                    }
                }
            break;
            case 2:
                bullets += 10;
            break;
        }
        
    }

    public void gettingTarget(){
        if(level == 1){
            targetPoint = targetPoint + 1;
            target_num++;
            if(target_num>=max_target_num){
                target_num_reached = true;
                CharacterController controller = player.GetComponent<CharacterController>();
                if (controller != null)
                {
                    controller.enabled = false; 
                    controller.transform.position = new Vector3(0f, 0f, -20f); 
                    controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
                    controller.enabled = true; 
                }
                else
                {
                    Debug.LogError("There is no CharacterController");
                }
            }
        }
    }

    public void CheckingLowWall(){
        checkingWall = false;
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; 
            controller.transform.position = new Vector3(0f, 0f, -20f); 
            controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            controller.enabled = true; 
        }
        else
        {
            Debug.LogError("There is no CharacterController");
        }
        lowWall.SetActive(false);
    }

    public void GettingEnemies(){

        switch(level){
            case 1:
                enemies_num_level1++;
            break;
            case 2:
                enemies_num_level2++;
                if(enemies_num_level2 == max_enemies_num_level2)
                   enemies_level2_alldie = true;
            break;
        }
        
    }
    //level1
    void OnButtonClickTuto(){
        level = 1;
        platformerPlayerController.enabled= true;
        tutorialObj.SetActive(true);
        begin.SetActive(false);
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; 
            controller.transform.position = new Vector3(0f, 0f, -20f); 
            controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            controller.enabled = true; 
        }
        else
        {
            Debug.LogError("There is no CharacterController");
        }
    }

    //level2

    void OnButtonClickPlay(){
        level = 2;
        platformerPlayerController.enabled = true;      
        begin.SetActive(false);
        playObj.SetActive(true);
        acutal_enemyObj2 = Instantiate(enemyObj2, new Vector3(0, 0, 0), Quaternion.Euler(0f,0,0f));
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled=false;
            controller.transform.position = initPos;
            controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            controller.enabled=true;
        }
        else
        {
            Debug.LogError("There is no CharacterController");
        }
    }

    void onButtonClickBegin(){
        gameOverPanel.SetActive(false);
        ChangeLevel();
    }

    public int GettingLevel(){
        return level;
    }

    //level2
    public void SettingInitalPlayer(){
        if(level == 2){

            health = health - 10;
            if(health <= 0){
                gameOverPanel.SetActive(true);
            }
            else{
                CharacterController controller = player.GetComponent<CharacterController>();
                if (controller != null)
                {
                    controller.enabled=false;
                    controller.transform.position = initPos;
                    controller.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    controller.enabled=true;
                }
                else
                {
                    Debug.LogError("There is no CharacterController");
                }
            }


            
        }
    }

    public void settingCoinObj(GameObject coin){
        coins.Add(coin);
    }

    public int GettingBullets(){
        return bullets;
    }

    public void ReducingBullets(){
        bullets--;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        if(level ==1){
            GUI.Label(new Rect(0, 10, 100, 200),"Point: "+point+"",labelStyle);
        }
        else if(level ==2){
            GUI.Label(new Rect(0, 10, 100, 200),"Health: "+health+"",labelStyle);
        }
        
        if(level == 1){
            GUI.Label(new Rect(0, 50, 100, 200),"Target: "+targetPoint+"",labelStyle);
        }
        else if(level ==2){
            GUI.Label(new Rect(0, 50, 100, 200),"Remaining bullets: "+bullets+"",labelStyle);
        }
        
        if(level == 1){
            GUI.Label(new Rect(0,100, 100, 200),"Enemy: "+(max_enemies_num_level1 - enemies_num_level1)+" ",labelStyle);
        }
        else if(level ==2){
            GUI.Label(new Rect(0,100, 100, 200),"Enemy: "+(max_enemies_num_level2 - enemies_num_level2)+" ",labelStyle);
        }
        
    }
}
