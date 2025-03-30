using UnityEngine;

public class Button_3_moving : MonoBehaviour
{

    private float z;
    [SerializeField] private float min_z = 35f;
    [SerializeField] private float max_z = 45f;
    private bool z_left_moving;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        z = transform.position.z;
        speed = 3f;
        z_left_moving = false;
        int checking = Random.Range(0, 2);
        if(checking ==0){
            z_left_moving = false;
        }
        else{
            z_left_moving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(z_left_moving){
            z += Time.deltaTime * speed;
            if(z>=max_z){
                z = max_z;
                z_left_moving = false;
            }
            float x = transform.position.x;
            float y = transform.position.y;
            transform.position = new Vector3(x, y,z);
        }
        else{
            z -= Time.deltaTime * speed;
            if(z<=min_z){
                z = min_z;
                z_left_moving = true;
            }
            float x = transform.position.x;
            float y = transform.position.y;
            transform.position = new Vector3(x, y,z);
        }
    }
}
