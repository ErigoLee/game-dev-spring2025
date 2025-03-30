using UnityEngine;

public class Button_1_moving : MonoBehaviour
{
    private float x;
    [SerializeField] private float min_x = -50f;
    [SerializeField] private float max_x = -40f;
    private bool x_left_moving;
    private float speed;
    void Start()
    {
        x = transform.position.x;
        speed = 3f;
        x_left_moving = false;
        int checking = Random.Range(0, 2);
        if(checking==0){
            x_left_moving = false;
        }
        else{
            x_left_moving = true;
        }
    }

    
    void Update()
    {
        if(x_left_moving){
            x -= Time.deltaTime * speed;
            if(x<=min_x){
                x = min_x;
                x_left_moving = false;
            }
            float y = transform.position.y;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);

        }else{
            x += Time.deltaTime * speed;
            if(x>=max_x){
                x = max_x;
                x_left_moving = true;
            }
            float y = transform.position.y;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }
    }
}
