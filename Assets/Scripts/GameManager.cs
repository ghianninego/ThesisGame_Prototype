using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject[] things;
	public GameObject goal;

	//UI gameObjects
	public GameObject pauseMenu;
	public GameObject yesNoMenu;
	public GameObject normalMenu;



	void Start(){
		if (Instance == null) {
			Instance = this.GetComponent<GameManager>();
		}

		Time.timeScale = 1;
		Menus ();


		things = GameObject.FindGameObjectsWithTag ("Goal");
		goal = things [Random.Range (0, things.Length)];
		goal.GetComponent<Goal> ().IsGoal = true;
		Debug.Log (goal.name);
	}

	void Menus() {
		pauseMenu.SetActive (false);
		yesNoMenu.SetActive (false);
		normalMenu.SetActive (false);
	}



}
