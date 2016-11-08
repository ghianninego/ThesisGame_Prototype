using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public bool IsGoal = false;
	public bool IsPosonous;

	void Start () {
		if (Random.Range (0, 2) == 1) {
			IsPosonous = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerScript> ().carrying == null) {
				GameManager.Instance.yesNoMenuDescription.text = "Do you want to get this mushroom?";
				GameManager.Instance.yesNoMenu.SetActive (true);
				GameManager.Instance.optionWindowIsActive = true;
				GameManager.Instance.getItem (gameObject);
				StartCoroutine (Pause ());
			}
			else {
				GameManager.Instance.randomMenuDescription.text = "You are only allowed to carry one mushroom.";
				GameManager.Instance.randomMenu.SetActive (true);
			}
		}
	}

	IEnumerator Pause(){
		Time.timeScale = 0.2f;
		yield return null;
	}


}
