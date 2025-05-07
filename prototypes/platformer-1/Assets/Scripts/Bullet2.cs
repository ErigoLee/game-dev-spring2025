using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody rb;

    //gameManager - player
    private GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, 3.0f);
        gameManager = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player_box")){
            gameManager.ReducingPlayerHP();
            Destroy(gameObject);
        }
    }
    
}
