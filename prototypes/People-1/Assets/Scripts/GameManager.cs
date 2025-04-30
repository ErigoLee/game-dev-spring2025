using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void DialogTalkPolice(){
        player.enabled = false;
        dialogueManager.TalkingPolice();
    }

    public void EndDialogTalkPolice(){
        player.enabled=true;
    }


    public void DialogTalkWoman(){
        player.enabled = false;
        dialogueManager.TalkingWomen();
    }

    public void EndDialogTalkWoman(){
        player.enabled = true;
    
    }

    public void DialogTalkDoctor(){
        player.enabled = false;
        dialogueManager.TalkingDoctor();
    }

    public void EndDialogTalkDoctor(){
        player.enabled = true;
    }

    public void DialogTalkBoy(){
        player.enabled = false;
        dialogueManager.TalkingBoy();
    }

    public void EndDialogTalkBoy(){
        player.enabled = true;
    }

    public void DialogTalkOldWoman(){
        player.enabled = false;
        dialogueManager.TalkingOldWoman();
    }

    public void EndDialogTalkOldWoman(){
        player.enabled = true;
    }
}
