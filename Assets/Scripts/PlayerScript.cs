﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject carrying;
	public GameObject victory;
	public GameObject notVictory;

	void Start () {
		carrying = null;
		// PlayerData DataOfPlayer = new PlayerData (insertDataHere);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Witch") {
			if (carrying!=null && this.carrying.GetComponent<Goal> ().IsGoal) {
				victory.SetActive (true);
			} else {
				notVictory.SetActive (true);
				carrying = null;
			}
		}
	}
}
