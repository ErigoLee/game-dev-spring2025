using UnityEngine;

public class RedBrick : MonoBehaviour
{
    private bool upper;
    private int brickSpeed;
    void Start()
    {
        upper = true;
        brickSpeed = 2;
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1){
            upper = false;
        }
        else{
            upper = true;
        }
    }

    
    void Update()
    {
        if(upper == true){
            if(transform.position.y >= 18.0f)
            {
                upper = false;
                
            }
            else{
                Vector3 newPosition = transform.position;
                newPosition.y += Time.deltaTime * brickSpeed;
                transform.position = newPosition;
            }
        }
        else{
            if(transform.position.y <= 16.0f)
            {
                upper = true;
            }
            else{
                Vector3 newPosition = transform.position;
                newPosition.y -= Time.deltaTime * brickSpeed;
                transform.position = newPosition;
            }
        }
    }
}
