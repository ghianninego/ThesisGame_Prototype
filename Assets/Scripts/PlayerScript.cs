using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject carrying;
	public GameObject victory;
	public GameObject notVictory;

	void Start () {
		carrying = null;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Witch") {
			if (this.carrying.GetComponent<Goal> ().IsGoal) {
				victory.SetActive (true);
			} else {
				notVictory.SetActive (true);
			}
		}
	}
}
