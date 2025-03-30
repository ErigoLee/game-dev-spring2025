using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        GameObject obj = GameObject.FindWithTag("GM");
        gameManager = obj.GetComponent<GameManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet")){
            gameManager.gettingTarget();
            Destroy(gameObject);
        }
    }
}
