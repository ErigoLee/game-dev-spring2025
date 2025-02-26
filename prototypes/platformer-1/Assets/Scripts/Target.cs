using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet")){
            gameManager.gettingTarget();
            Destroy(gameObject);
        }
    }
}
