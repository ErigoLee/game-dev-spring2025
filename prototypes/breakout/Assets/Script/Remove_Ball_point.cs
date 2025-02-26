using UnityEngine;

public class Remove_Ball_point : MonoBehaviour
{  

    private GameObject gameManagerObj;
    private GameManager gameManager;
    void Start(){
        gameManagerObj = GameObject.Find("Game_Manager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("ball")){
            Destroy(other.gameObject);
            gameManager.LoseBallSignal();
        }
    }
}
