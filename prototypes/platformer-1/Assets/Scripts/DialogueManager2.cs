using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DialogueManager2 : MonoBehaviour
{
    //TextObj
	[SerializeField] private GameObject textObj;
	[SerializeField] private TextMeshProUGUI dialogueText;

	//Image
	[SerializeField] private GameObject npc;
	[SerializeField] private GameObject character;
	[SerializeField] private GameObject left;
	[SerializeField] private GameObject right;

    //Talking NPC1
	private int countingTalkNPC1 = 0;
	private int maxCountingTalkNPC1 = 6;

	//Talking NPC2
	private int countingTalkNPC2 = 0;
	private int maxCountingTalkNPC2 = 11;

	//Game Manager
	[SerializeField] private GameManager gameManager;

	//talking dialogueNum
	private int dialogueNum = -1;

    //dialogue text
    private string [] line1 = {"Hey there! I'm Tommy.\nWelcome to this place!","Hi, Tommy!\nNice to meet you.","This is the training area.\nPractice hard and\nI'll see you again in the real game world!","Oh, okay!\nSounds good~","I’ll be cheering for you to complete\nall your training successfully!","Thanks, Tommy!\nSee you again soon!"};

    private string [] line2 = {"Hey, I'm Tommy. Still breathing, huh?\nYou must be a decent fighter.","Tommy? Who are you?\nWhat is this place?","This is the Edge of Locks.\nA world where monsters guard the keys.","Keys...?\nIs that what I’m looking for?","That’s right. You’ll need a key to escape this world.\nBut there’s more than just one.","So...\nI have to defeat all the monsters?","Exactly.\nThe keys are only granted to the strong.","It won’t be easy,\nbut there’s no turning back now.","Remember,\nthe key isn’t just a piece of metal.","What do you mean?\nIs there something hidden?","You’ll see soon enough...\nMaybe you’re the one who can handle it."};

    void Update(){
        switch(dialogueNum){
            case 0:
            TalkingNPCLevel1Continue();
            break;
            case 1:
            TalkingNPCLevel2Continue();
            break;
        }
    }

    public void TalkingNPCLevel1(){
		dialogueNum = 0;
		npc.SetActive(true);
		character.SetActive(true);
	}

    public void TalkingNPCLevel1End(){
		dialogueNum = -1;
		countingTalkNPC1 = 0;
		npc.SetActive(false);
		character.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
        textObj.SetActive(false);
		gameManager.EndDialogue();
	}

    public void TalkingNPCLevel2(){
		dialogueNum = 1;
		npc.SetActive(true);
		character.SetActive(true);
	}

	public void TalkingNPCLevel2End(){
		dialogueNum = -1;
		countingTalkNPC2 = 0;
		npc.SetActive(false);
		character.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
        textObj.SetActive(false);
		gameManager.EndDialogue();
	}

	public void TalkingNPCLevel1Continue(){
		//First dialogue
		if(Input.GetKeyDown(KeyCode.D)){
			if(countingTalkNPC1>=maxCountingTalkNPC1){
				TalkingNPCLevel1End();
			}
			else{
				//string line = DialogueManager.scc.getSCCLine("NPC1");
				
                if(countingTalkNPC1 % 2 ==0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					Debug.Log("NPC says: " + line1[countingTalkNPC1]);
					dialogueText.text = line1[countingTalkNPC1];
					countingTalkNPC1++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					Debug.Log("Character says: "+line1[countingTalkNPC1]);
					dialogueText.text = line1[countingTalkNPC1];
					countingTalkNPC1++;
				}
			}
		}
	}

	public void TalkingNPCLevel2Continue(){

		if(Input.GetKeyDown(KeyCode.D)){
			if(countingTalkNPC2>=maxCountingTalkNPC2){
				TalkingNPCLevel2End();
			}
			else{
				//string line = DialogueManager.scc.getSCCLine("NPC2");
				
                if(countingTalkNPC2 % 2 ==0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					Debug.Log("NPC says: " + line2[countingTalkNPC2]);
					dialogueText.text = line2[countingTalkNPC2];
					countingTalkNPC2++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					Debug.Log("Character says: "+line2[countingTalkNPC2]);
					dialogueText.text = line2[countingTalkNPC2];
					countingTalkNPC2++;
				}
			}
		}
	}
}
