using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject carrying;
	void Start () {
		carrying = null;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Witch") {
			//create a dialog Box for the witch is that it? 
			//if (carrying.IsGoal) say you got it else thats not it
		}
	}
}
