using UnityEngine;
using System.Collections;

public class IntroButtons : MonoBehaviour {

	public GameObject PlayerDataInputPanel;
	public GameObject IntroPanel;

	//Input Game Objects
	public UILabel name;
	public UILabel id;

	public void IntroSubmit() {
		string _name = name.text;
		string _id = id.text;

		if (CheckInput (_name, _id) == true) {
			//submit data

			PlayerDataInputPanel.SetActive (false);
			IntroPanel.SetActive (true);
		} else {
			Debug.Log ("Invalid Input");
		}

		Debug.Log (_name);
		Debug.Log (_id);

	}

	public void IntroClose() {
		GameManager.Instance.IntroductionWindow.SetActive (false);
		Time.timeScale = 1;

		GameManager.Instance.introWindow = false;
	}

	bool CheckInput(string n, string i) {
		if (n.Equals("") || n.Equals("Enter name here") || i.Equals("") || i.Equals("Enter ID here")) {
			return false;
		} else {
			return true;
		}
	}
}
