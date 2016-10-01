using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	public delegate void Yes(GameObject x);
	public static event Yes yesButton;
	public delegate void No();
	public static event No noButton;

	void Start(){
		if (yesButton != null) {
			yesButton (null);
		}
		if (noButton != null) {
			noButton ();
		}
	}

	void OnEnable(){
		yesButton += ClickedYes;
		noButton += ClickedNo;
	}

	void OnDisable(){
		yesButton -= ClickedYes;
		noButton -= ClickedNo;
	}

	public static void ClickedYes(GameObject mushroom){
		PlayerScript player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		if(player.carrying == null){
			player.carrying = mushroom;
			mushroom.SetActive (false);

		}
		Time.timeScale = 1;
		GameObject.FindGameObjectWithTag ("YesNoWindow").SetActive (false);
	}

	public static void ClickedNo(){
		Time.timeScale = 1;
		GameObject.FindGameObjectWithTag ("YesNoWindow").SetActive (false);
	}


}
