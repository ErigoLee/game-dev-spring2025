using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody rb;

    //Enemy
    private Enemy enemy;
    private EnemyAI enemyAI;

    private SoundManager soundManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, 3.0f);
        GameObject smObject = GameObject.FindWithTag("SM"); // Find the object with tag "SM"
        if (smObject != null)
        {
            soundManager = smObject.GetComponent<SoundManager>();
        }
        else
        {
            Debug.LogWarning("No GameObject with the tag 'SM' found.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target")){
            if(soundManager != null){
                soundManager.ShootBumpEffect();
            }
            Destroy(gameObject);
        }

        if(other.CompareTag("Enemy")){
            if(soundManager != null){
                soundManager.ShootBumpEffect();
            }
            enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.MakingCoin();
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            enemyAI = other.gameObject.GetComponent<EnemyAI>();
            if(enemyAI != null){
                enemyAI.MakingCoin();
                print("Die!!");
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            
        }
    }
}
