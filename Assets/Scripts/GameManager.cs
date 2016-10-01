using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public GameObject[] things;
	public GameObject goal;

	//UI gameObjects
	public GameObject pauseMenu;
	public GameObject yesNoMenu;
	public GameObject normalMenu;
	private GameObject item;



	void Start(){

		if (Instance == null) {
			Instance = this;
			Instance.gameObject.SetActive (true);
		} 

		Time.timeScale = 1;
		Menus ();


		things = GameObject.FindGameObjectsWithTag ("Goal");
		goal = things [Random.Range (0, things.Length)];
		goal.GetComponent<Goal> ().IsGoal = true;
		Debug.Log (goal.name);
		Debug.Log ("asasasa");

	}

	void Menus() {
		pauseMenu.SetActive (false);
		yesNoMenu.SetActive (false);
		normalMenu.SetActive (false);
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
