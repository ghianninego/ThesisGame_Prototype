using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject[] things;
	public GameObject goal;

	//UI gameObjects
	public GameObject pauseMenu;
	public GameObject yesNoMenu;
	public GameObject victoryMenu;
	public GameObject notVictoryMenu;
	public GameObject playersEmotionMenu;
	private GameObject item;


	void Start(){
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (Instance);
		}

		Time.timeScale = 1;
		SetMenus ();

		things = GameObject.FindGameObjectsWithTag ("Goal");
		goal = things [Random.Range (0, things.Length)];
		goal.GetComponent<Goal> ().IsGoal = true;
		Debug.Log (goal.name);
	//	Debug.Log ("asasasa");

	}

	void SetMenus() {
		pauseMenu.SetActive (false);
		yesNoMenu.SetActive (false);
		notVictoryMenu.SetActive (false);
		victoryMenu.SetActive (false);
	}

	public void getItem(GameObject x){
		item = x;
	}

	public void ButtonYes(){
		Buttons.ClickedYes (item);
	}

	public void ButtonNo(){
		Buttons.ClickedNo ();
	}


}
