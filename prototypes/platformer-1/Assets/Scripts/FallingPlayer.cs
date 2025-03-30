using UnityEngine;

public class FallingPlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            gameManager.SettingInitalPlayer();
        }
    }
}
