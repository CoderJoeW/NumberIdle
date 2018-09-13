using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObject : MonoBehaviour {
	[SerializeField]
	private float amount = 0;

	[SerializeField]
	private string type = "";

	private Text btnText;
	private Button btn;

	private void Start(){
		btn = this.gameObject.transform.GetComponent<Button> ();
		btn.onClick.AddListener (delegate {
			switch(type){
			case "Clicker":
				GameManager.Instance.Clicker();
				break;
			case "Clicker Progress":
				GameManager.Instance.BuyClicker(amount);
				break;
				case "Offline Progress":
				GameManager.Instance.BuyOffline(amount);
				break;
			}
		});

		btnText = this.gameObject.GetComponentInChildren<Text> ();
	}

	private void Update(){
		if (type != "Clicker") {
			if (type == "Clicker Progress") {
				if (GameManager.Instance.GetUnits () < GameManager.Instance.GetCost (GameManager.Instance.clickerBaseCost, GameManager.Instance.clickerExponentialCost, GameManager.Instance.GetClickerProgress () + amount)) {
					btn.gameObject.GetComponent<Image> ().color = Color.red;
					btn.interactable = false;
				} else {
					btn.gameObject.GetComponent<Image> ().color = Color.green;
					btn.interactable = true;
				}
				btnText.text = type + " X" + amount + " Cost: " + GameManager.Instance.GetCost (GameManager.Instance.clickerBaseCost, GameManager.Instance.clickerExponentialCost, GameManager.Instance.GetClickerProgress () + amount);		
			} else if (type == "Offline Progress") {
				if (GameManager.Instance.GetUnits () < GameManager.Instance.GetCost (GameManager.Instance.offlineBaseCost, GameManager.Instance.offlineExponentialCost, GameManager.Instance.GetOfflineProgress() + amount)) {
					btn.gameObject.GetComponent<Image> ().color = Color.red;
					btn.interactable = false;
				} else {
					btn.gameObject.GetComponent<Image> ().color = Color.green;
					btn.interactable = true;
				}
				btnText.text = type + " X" + amount + " Cost: " + GameManager.Instance.GetCost (GameManager.Instance.offlineBaseCost, GameManager.Instance.offlineExponentialCost, GameManager.Instance.GetOfflineProgress() + amount);
			}
		}
	}
}
