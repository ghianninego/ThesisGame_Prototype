using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public static Buttons Instance = null;
	public delegate void Yes(GameObject x);
	public static event Yes yesButton;
	public delegate void No();
	public static event No noButton;

	void Start(){

		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (Instance);
		}

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

	public void ClickedYes(GameObject mushroom){
		PlayerScript player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		if(player.carrying == null && mushroom!=null){
			player.carrying = mushroom;
			mushroom.SetActive (false);

		}
		Time.timeScale = 1;

			GameManager.Instance.yesNoMenu.SetActive (false);

	}

	public void ClickedNo(){
		Time.timeScale = 1;

			GameManager.Instance.yesNoMenu.SetActive (false);

	}

	public void victoryClose(GameObject window){
		window.SetActive (false);
	}

	public void notVictoryClose(GameObject window){
		window.SetActive (false);
		PlayerScript player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		player.carrying = null;
	}

}
