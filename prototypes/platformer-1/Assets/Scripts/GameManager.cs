using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GUISkin skin;


    private int point;
    private int health;

    private int targetPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        point = 0;
        health = 100;
        targetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gettingPoint(){
        point = point + 10;
    }

    public void losingHealth(){
        health = health - 10;
    }

    public void gettingTarget(){
        targetPoint = targetPoint + 1;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        GUI.Label(new Rect(0, 10, 100, 200),"Point: "+point+"",labelStyle);
        GUI.Label(new Rect(0, 50, 100, 200),"Health: "+health+"",labelStyle);
        GUI.Label(new Rect(0, 100, 100, 200),"Target: "+targetPoint+"",labelStyle);
    }
}
