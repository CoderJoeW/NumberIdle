using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{
	public DateTime lastSave;
	public float currentUnits;
	public float clickerProgress;
	public float offlineProgress;

	public GameData(DateTime last_save,float current_units,float clicker_progress,float offline_progress){
		lastSave = last_save;
		currentUnits = current_units;
		clickerProgress = clicker_progress;
		offlineProgress = offline_progress;
	}
}

public class SaveManager : MonoBehaviour {
	private const string DATA_FILE = "saveData.number";
	private string destination = "";

	private float timer = 5;
	private float timerReset = 5;

	private void Awake(){
		destination = Application.persistentDataPath + DATA_FILE;
		LoadData ();
	}

	private void Update(){
		if (timer > 0) {
			timer -= 1 * Time.deltaTime;
		}

		if (timer <= 0) {
			SaveData ();
			timer = timerReset;
		}
	}

	private void SaveData(){
		FileStream file;

		if (File.Exists (destination)) {
			file = File.OpenWrite (destination);
		} else {
			file = File.Create (destination);
		}

		GameData data = new GameData (DateTime.Now, GameManager.Instance.GetUnits (), GameManager.Instance.GetClickerProgress (), GameManager.Instance.GetOfflineProgress ());

		BinaryFormatter bf = new BinaryFormatter ();
		bf.Serialize (file, data);
		file.Close ();
	}

	private void LoadData(){
		FileStream file;

		if (File.Exists (destination)) {
			file = File.OpenRead (destination);
		} else {
			GameManager.Instance.Firstload ();
			return;
		}

		BinaryFormatter bf = new BinaryFormatter ();
		GameData data = (GameData)bf.Deserialize (file);
		file.Close ();

		GameManager.Instance.SetLastSave (data.lastSave);
		GameManager.Instance.SetData (data.currentUnits, data.clickerProgress, data.offlineProgress);
	}

	private void ResetData(){
		if (File.Exists (destination)) {
			try{
				File.Delete(destination);
				LoadData();
			}catch(Exception e){
				Debug.LogError ("Msg: " + e);
			}
		}
	}
}
