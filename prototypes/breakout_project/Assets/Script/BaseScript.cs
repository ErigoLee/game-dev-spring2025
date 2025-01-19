using UnityEngine;

public class script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       // if left is pressed, move left
       if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 newPosition = transform.position;
            newPosition.x -= 1;
            transform.position = newPosition;
        }
       // if right is pressed, move right
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 newPosition = transform.position;
            newPosition.x += 1;
            transform.position = newPosition;
        }
    }
}
