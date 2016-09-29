using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject[] things;
	public GameObject goal;
	public GameObject pauseMenu;


	void Start(){
		if (Instance == null) {
			Instance = this.GetComponent<GameManager>();
		}

		Time.timeScale = 1;

		pauseMenu.SetActive (false);

		things = GameObject.FindGameObjectsWithTag ("Goal");
		goal = things [Random.Range (0, things.Length + 1)];
		goal.GetComponent<Goal> ().IsGoal = true;
		Debug.Log (goal.name);
	}



}
