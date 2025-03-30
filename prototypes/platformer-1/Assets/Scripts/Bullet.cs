using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody rb;

    //Enemy
    private Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, 3.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target")){
            
            Destroy(gameObject);
        }

        if(other.CompareTag("Enemy")){
            enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.MakingCoin();
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            
        }
    }
}
