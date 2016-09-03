using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject pauseMenu;
	public UILabel item1;
	public UILabel item2;
	public UILabel item3;
	public UILabel item4;
	public UILabel item5;
	public UILabel item6;
	public static GameObject[] thingsToGet;

	void OnEnable() {
		if (Instance == null) {
			Instance = this;
		}
	}

	void OnDisable() {
		if (Instance == this) {
			Instance = null;
		}
	}

	void Awake(){
		thingsToGet = GameObject.FindGameObjectsWithTag ("Objects");
		pauseMenu.SetActive (false);
	}

	/**
	void Update(){
		for (int i = 0; i < thingsToGet.Length; i++) {
			Debug.Log (thingsToGet[i].name);
		}
	}
	**/

}
