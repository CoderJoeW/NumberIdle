using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager: Singleton<GameManager>{
	private float currentUnits = 0;
	private float clickerProgress = 1;
	private float offlineProgress = 1;

	public float clickerBaseCost = 2;
	public float clickerExponentialCost = 1.07f;

	public float offlineBaseCost = 5;
	public float offlineExponentialCost = 1.15f;

	private DateTime lastSave = new DateTime ();

	public void ModifyUnits(float amount,string process){
		if (process == "add") {
			currentUnits += amount;
		} else if (process == "sub") {

		} else if (process == "mul") {

		} else if (process == "div") {

		}
	}

	public void Clicker(){
		ModifyUnits (clickerProgress, "add");
	}

	public void BuyClicker(float amount){
		if (currentUnits >= GetCost (clickerBaseCost, clickerExponentialCost, clickerProgress + amount)) {
			currentUnits -= GetCost (clickerBaseCost, clickerExponentialCost, clickerProgress + amount);
			clickerProgress += amount;
		} else {
			Debug.Log ("User does not have enough units to purchase clickers");
		}
	}

	public void BuyOffline(float amount){
		if (currentUnits >= GetCost (offlineBaseCost, offlineExponentialCost, offlineProgress + amount)) {
			currentUnits -= GetCost (offlineBaseCost, offlineExponentialCost, offlineProgress + amount);
			offlineProgress += amount;
		} else {
			Debug.Log ("User does not have enough units to purchase offline progress");
		}
	}

	public float GetCost(float baseCost,float exponentialGain,float amountOwned){
		float returnVal = 0;
		for (int i = 0; i < amountOwned; i++) {
			returnVal += (amountOwned * exponentialGain);
		}

		returnVal = (returnVal * baseCost);

		return returnVal;
	}

	public void Firstload(){
		lastSave = DateTime.Now;
		currentUnits = 0;
		offlineProgress = 1;
		clickerProgress = 1;
	}

	public float GetUnits(){
		return currentUnits;
	}

	public float GetClickerProgress(){
		return clickerProgress;
	}

	public float GetOfflineProgress(){
		return offlineProgress;
	}

	public void SetLastSave(DateTime val){
		lastSave = val;
	}

	public DateTime GetLastSave(){
		return lastSave;
	}

	public void SetData(float units,float clicker_Progress,float offline_Progress){
		currentUnits = units;
		clickerProgress = clicker_Progress;
		offlineProgress = offline_Progress;
	}
}
