using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {
	public string Name;
	public string NPCname;
	public string Description;
	public string Catagory;
	public string Requirement;
	public int RequireNumber;
	public int QuestCounter;
	public bool IsAccept = false;
	public bool IsComplete = false;

	public Quest(string name,string npc){
		Name = name;
		NPCname = npc;
		QuestCounter = 0;
	}

	public void SetDescription(string inputDes){
		Description = inputDes;
	}

	public void SetRequirement(string inputCat, string inputReq, int inputReqs){
		Catagory = inputCat;
		Requirement = inputReq;
		RequireNumber = inputReqs;
	}

	public void AcceptQuest(){
		IsAccept = true;
	}

	public void QuestCompleteCheck(){
		if (QuestCounter >= RequireNumber)
			IsComplete = true;
		else
			IsComplete = false;
	}

	public void CounterUpdate(int InputCounter){
		QuestCounter = InputCounter;
	}
}
