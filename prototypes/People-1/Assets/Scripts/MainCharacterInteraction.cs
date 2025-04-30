using UnityEngine;

public class MainCharacterInteraction : MonoBehaviour
{
    //target characters
    [SerializeField] private Transform police;
    [SerializeField] private Transform doctor;
    [SerializeField] private Transform boy;
    [SerializeField] private Transform elder_Female;
    [SerializeField] private Transform female;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameManager gameManager;
    //person
    [SerializeField] private Transform male;
    private float interactionDistance = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance(police, "Police",1);
        CheckDistance(doctor, "Doctor",2);
        CheckDistance(boy, "Boy",3);
        CheckDistance(elder_Female, "Elder Female",4);
        CheckDistance(female, "Female",5);
    }

    private void CheckDistance(Transform target, string targetName, int idx)
    {
        if (target == null) return; // 안전 처리

        float distance = Vector3.Distance(male.position, target.position);
        //Debug.Log($"{targetName} : {distance}");
        if (distance <= interactionDistance)
        {
            //Debug.Log("inside distance!");
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == target)
                    {
                        Debug.Log($"[SUCCESS] You clicked on {targetName} within {interactionDistance}m! (Actual Distance: {distance:F2})");
                        
                        switch(idx){
                            case 1:
                                gameManager.DialogTalkPolice();
                            break;
                            case 2:
                                gameManager.DialogTalkDoctor();
                            break;
                            case 3:
                                gameManager.DialogTalkBoy();
                            break;
                            case 4:
                                gameManager.DialogTalkOldWoman();
                            break;
                            case 5:
                                gameManager.DialogTalkWoman();
                            break;
                        }
                        
                    }
                }
            }
        }
        else
        {
            
            //Debug.Log($"{targetName} is too far. Distance: {distance:F2}");
        }
    }
}
