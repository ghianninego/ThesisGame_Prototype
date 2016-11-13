using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject[] things;
	public GameObject goal;

	//UI Game Objects
	public GameObject pauseMenu;
	public GameObject yesNoMenu;
	public UILabel yesNoMenuDescription;
	public GameObject victoryMenu;
	public GameObject notVictoryMenu;
	public GameObject playersEmotionMenu;
	public GameObject randomMenu;
	public GameObject IntroductionWindow;
	public UILabel randomMenuDescription;

	//Emotion Game Objects
	public UILabel emotionLabel;
	public UISprite emotionSprite;
	[HideInInspector] public Sprite[] allEmotionSprites;

	public bool optionWindowIsActive;
	public bool introWindow = true;
	private GameObject item;

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

	void Start(){
		allEmotionSprites = Resources.LoadAll<Sprite> ("emotions");

		if (introWindow == true) {
			Time.timeScale = 0;
			IntroductionWindow.SetActive (true);
		} else {
			Time.timeScale = 1;
		}

		SetMenus ();
		optionWindowIsActive = false;

		//Emotion sprite based from emotion recognition system


		//Goals
		things = GameObject.FindGameObjectsWithTag ("Goal");
		goal = things [Random.Range (0, things.Length)];
		goal.GetComponent<Goal> ().IsGoal = true;
		Debug.Log (goal.name);
	}

	void SetMenus() {
		pauseMenu.SetActive (false);
		yesNoMenu.SetActive (false);
		notVictoryMenu.SetActive (false);
		victoryMenu.SetActive (false);
		playersEmotionMenu.SetActive (false);
		randomMenu.SetActive (false);
	}

	public void getItem(GameObject x){
		item = x;
	}

	public void ButtonYes(){
		Buttons.Instance.ClickedYes (item);
	}

	public void ButtonNo(){
		Buttons.Instance.ClickedNo ();
	}

	public void SetEmotionSprite(char emotion) {
		emotionSprite.spriteName = char.ToString(emotion);
	}


}
