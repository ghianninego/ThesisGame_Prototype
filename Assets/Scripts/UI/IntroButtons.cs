using UnityEngine;
using System.Collections;

public class IntroButtons : MonoBehaviour {

	public GameObject PlayerDataInputPanel;
	public GameObject IntroPanel;

	//Input Game Objects
	public UILabel name;
	public UILabel id;
	public UILabel section;

	public void IntroSubmit() {
		string _name = name.text;
		string _id = id.text;
		string _section = section.text;

		if (CheckInput (_name, _id) == true) {
			//submit data

			PlayerDataInputPanel.SetActive (false);
			Done ();
			IntroPanel.SetActive (true);


		} else {
			Debug.Log ("Invalid Input");
		}




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

	public void Done(){
		if (!PlayerPrefs.HasKey ("PLAYERID")) {
			PlayerPrefs.SetInt ("PLAYERID", 0);
		} else {
			PlayerPrefs.SetInt ("PLAYERID", PlayerPrefs.GetInt("PLAYERID")+1);
		}

		PlayerData.SetPlayerData (id.text, name.text, section.text, PlayerPrefs.GetInt ("PLAYERID")+"");
	}
}
