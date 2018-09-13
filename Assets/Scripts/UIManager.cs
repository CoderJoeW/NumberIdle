using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
	[SerializeField]
	private Text currentUnitsText;

	[SerializeField]
	private Text clickerProgressText;

	[SerializeField]
	private Text offlineProgressText;

	private void Update(){
		currentUnitsText.text = "Units: " + GameManager.Instance.GetUnits ();
		clickerProgressText.text = "Offline Progress: " + GameManager.Instance.GetOfflineProgress () + "upc";
		offlineProgressText.text = "Clicker Progress: " + GameManager.Instance.GetClickerProgress () + "upm";
	}
}
