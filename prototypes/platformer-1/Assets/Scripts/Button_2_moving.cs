using UnityEngine;

public class Button_2_moving : MonoBehaviour
{
    private float y;
    [SerializeField] private float min_y = 8f;
    [SerializeField] private float max_y = 10f;

    private bool y_up_moving;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        y = transform.position.y;
        speed = 1.5f;
        y_up_moving = false;
        int checking = Random.Range(0, 2);
        if (checking == 0){
            y_up_moving = false;
        }
        else{
            y_up_moving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (y_up_moving){
            y += Time.deltaTime * speed;
            if(y >= max_y){
                y = max_y;
                y_up_moving = false;
            }
            float x = transform.position.x;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }else{
            y -= Time.deltaTime * speed;
            if (y <= min_y){
                y = min_y;
                y_up_moving = true;
            }
            float x = transform.position.x;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }
    }
}
