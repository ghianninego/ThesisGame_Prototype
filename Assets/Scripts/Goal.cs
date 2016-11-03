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
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerScript> ().carrying == null) {
				GameManager.Instance.yesNoMenu.SetActive (true);
				GameManager.Instance.optionWindowIsActive = true;
				GameManager.Instance.getItem (gameObject);
				StartCoroutine (Pause());
			}
		}
	}

	IEnumerator Pause(){
		Time.timeScale = 0.2f;
		yield return null;
	}


}
