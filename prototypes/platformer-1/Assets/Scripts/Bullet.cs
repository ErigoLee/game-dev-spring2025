using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target")){
            
            Destroy(gameObject);
        }
    }
}
