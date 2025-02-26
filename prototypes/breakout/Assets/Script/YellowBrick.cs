using UnityEngine;

public class YellowBrick : MonoBehaviour
{
    private bool upper;
    private int brickSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        if(upper == true){
            if(transform.position.y >= 10.0f)
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
            if(transform.position.y <= 8.0f)
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
