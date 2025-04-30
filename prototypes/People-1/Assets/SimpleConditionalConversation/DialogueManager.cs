using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public static SimpleConditionalConversation scc;

	public static Action<string, string> DialogueAction;

	// NOTE: When you do not use the google sheet option, it is expecting the file
	// to be named "data.csv" and for it to be in the Resources folder in Assets.
	public bool useGoogleSheet = false;
	public string googleSheetDocID = "";
	[SerializeField] private GameObject textObj;
	[SerializeField] private TextMeshProUGUI dialogueText;
	
	//Image
	[SerializeField] private GameObject man;
	[SerializeField] private GameObject woman;
	[SerializeField] private GameObject boy;
	[SerializeField] private GameObject oldWoman;
	[SerializeField] private GameObject policeWoman;
	[SerializeField] private GameObject doctor;
	[SerializeField] private GameObject left;
	[SerializeField] private GameObject right;
	
	
	private int dialogueNum = -1;

	//Talking women
	private int countingTalkW = 0;
	private int maxCountingTalkW = 4;
	//Talking police
	private int countingTalkP = 0;
	private int maxCountingTalkP = 12;

	//Talking doctor
	private int countingTalkD = 0;
	private int maxCountingTalkD = 10;

	//Talking boy
	private int countingTalkB = 0;
	private int maxCountingTalkB = 11;

	//Talking old woman
	private int countingTalkOW = 0;
	private int maxCountingTalkOW = 11;

	//Game Manager
	[SerializeField] private GameManager gameManager;

	
	// Start is called before the first frame update
	void Start()
	{
		if (useGoogleSheet) {
			// This will start the asyncronous calls to Google Sheets, and eventually
			// it will give a value to scc, and also call LoadInitialHistory().
			GoogleSheetSimpleConditionalConversation gs_ssc = gameObject.AddComponent<GoogleSheetSimpleConditionalConversation>();
			gs_ssc.googleSheetDocID = googleSheetDocID;
		} else {
			scc = new SimpleConditionalConversation("data");
			LoadInitialSCCState();
		}
	}
	
	public static void LoadInitialSCCState()
	{
		// Example of setting the initial state:
		// NOTE: If you are putting a number or bool, make sure not to store them
		// as strings.
		//
		// scc.setGameStateValue("playerWearing", "equals", "Green shirt");
	}
	
	// Update is called once per frame
	void Update()
	{
		
		switch(dialogueNum){
			case 0:
				TalkingWomenContinue();
			break;
			case 1:
				TalkingPoliceContinue();
			break;
			case 2:
			    TalkingDoctorContinue();
			break;
			case 3:
				TalkingBoyContinue();
			break;
			case 4:
				TalkingOldWomanContinue();
			break;
		}
	}
	//police
	public void TalkingPolice(){
		dialogueNum = 1;
		man.SetActive(true);
		policeWoman.SetActive(true);
	}

	public void TalkingPoliceEnd(){
		man.SetActive(false);
		policeWoman.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
		textObj.SetActive(false);
		dialogueNum = -1;
		gameManager.EndDialogTalkPolice();
	}
	public void TalkingPoliceContinue(){
		
		//First dialogue
		if (Input.GetKeyDown(KeyCode.D)){
			if (countingTalkP >= maxCountingTalkP){
				TalkingPoliceEnd();
			}
			else{
				if(countingTalkP == 0){
					SettingDefault();
				}
				string line = DialogueManager.scc.getSCCLine("B");
				if(countingTalkP % 2 == 0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					//dialoguePanel.material = dialoguePanelImageLeft;
					Debug.Log("Man says: " + line);
					dialogueText.text = line;
					countingTalkP++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					//dialoguePanel.material = dialoguePanelImageRight;
					Debug.Log("Police says: " + line);
					dialogueText.text = line;
					countingTalkP++;
				}
			}
			
		}

	}

	//woman
	public void TalkingWomen(){
		dialogueNum = 0;
		man.SetActive(true);
		woman.SetActive(true);
	}

	public void TalkingWomenEnd(){
		man.SetActive(false);
		woman.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
		textObj.SetActive(false);
		dialogueNum = -1;
		countingTalkW  = 0;
		gameManager.EndDialogTalkWoman();
	}

	public void TalkingWomenContinue(){
		
		//First dialogue
		if (Input.GetKeyDown(KeyCode.D)){
			if (countingTalkW >= maxCountingTalkW){
				TalkingWomenEnd();
			}
			else{
				if(countingTalkW == 0){
				SettingDefault();
				}
				string line = DialogueManager.scc.getSCCLine("A");
				if(countingTalkW % 2 == 0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					//dialoguePanel.material = dialoguePanelImageLeft;
					Debug.Log("Man says: " + line);
					dialogueText.text = line;
					countingTalkW++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					//dialoguePanel.material = dialoguePanelImageRight;
					Debug.Log("Woman says: " + line);
					dialogueText.text = line;
					countingTalkW++;
				}
			}
			
		}

	}

	//doctor
	
	public void TalkingDoctor(){
		dialogueNum = 2;
		doctor.SetActive(true);
		man.SetActive(true);
	}

	public void TalkingDoctorEnd(){
		man.SetActive(false);
		doctor.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
		textObj.SetActive(false);
		dialogueNum = -1;
		countingTalkD  = 0;
		gameManager.EndDialogTalkDoctor();
	}
	public void TalkingDoctorContinue(){
		
		//First dialogue
		if (Input.GetKeyDown(KeyCode.D)){
			if (countingTalkD >= maxCountingTalkD){
				TalkingDoctorEnd();
			}
			else{
				if(countingTalkD == 0){
				SettingDefault();
				}
				string line = DialogueManager.scc.getSCCLine("C");
				if(countingTalkD % 2 == 0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					//dialoguePanel.material = dialoguePanelImageLeft;
					Debug.Log("Man says: " + line);
					dialogueText.text = line;
					countingTalkD++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					//dialoguePanel.material = dialoguePanelImageRight;
					Debug.Log("Doctor says: " + line);
					dialogueText.text = line;
					countingTalkD++;
				}
			}
			
		}

	}

	//boy
	public void TalkingBoy(){
		dialogueNum = 3;
		boy.SetActive(true);
		man.SetActive(true);
	}

	public void TalkingBoyEnd(){
		man.SetActive(false);
		boy.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
		textObj.SetActive(false);
		dialogueNum = -1;
		countingTalkB  = 0;
		gameManager.EndDialogTalkBoy();
	}

	public void TalkingBoyContinue(){
		
		//First dialogue
		if (Input.GetKeyDown(KeyCode.D)){
			if (countingTalkB >= maxCountingTalkB){
				TalkingBoyEnd();
			}
			else{
				if(countingTalkB == 0){
				SettingDefault();
				}
				string line = DialogueManager.scc.getSCCLine("D");
				if(countingTalkB % 2 == 0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					//dialoguePanel.material = dialoguePanelImageLeft;
					Debug.Log("Man says: " + line);
					dialogueText.text = line;
					countingTalkB++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					//dialoguePanel.material = dialoguePanelImageRight;
					Debug.Log("Boy says: " + line);
					dialogueText.text = line;
					countingTalkB++;
				}
			}
			
		}

	}

	//old woman
	public void TalkingOldWoman(){
		dialogueNum = 4;
		oldWoman.SetActive(true);
		man.SetActive(true);
	}

	public void TalkingOldWomanEnd(){
		man.SetActive(false);
		oldWoman.SetActive(false);
		left.SetActive(false);
		right.SetActive(false);
		textObj.SetActive(false);
		dialogueNum = -1;
		countingTalkOW  = 0;
		gameManager.EndDialogTalkBoy();
	}
	public void TalkingOldWomanContinue(){
		
		//First dialogue
		if (Input.GetKeyDown(KeyCode.D)){
			if (countingTalkOW >= maxCountingTalkOW){
				TalkingOldWomanEnd();
			}
			else{
				if(countingTalkOW == 0){
				SettingDefault();
				}
				string line = DialogueManager.scc.getSCCLine("E");
				if(countingTalkOW % 2 == 0){
					textObj.SetActive(true);
					left.SetActive(true);
					right.SetActive(false);
					//dialoguePanel.material = dialoguePanelImageLeft;
					Debug.Log("Man says: " + line);
					dialogueText.text = line;
					countingTalkOW++;
				}
				else{
					textObj.SetActive(true);
					left.SetActive(false);
					right.SetActive(true);
					//dialoguePanel.material = dialoguePanelImageRight;
					Debug.Log("Old woman says: " + line);
					dialogueText.text = line;
					countingTalkOW++;
				}
			}
			
		}

	}


	private void SettingDefault(){
		scc.setGameStateValue("default", "equals", 0);
	}
}
