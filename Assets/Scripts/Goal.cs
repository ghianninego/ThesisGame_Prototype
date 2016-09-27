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
			//Insert A Window Option TakeIt or Leave it or Taste it
			//if (Take it) col.gameObject.carrying = this.gameObject;
			//if (Taste It) if (IsPosonous) = gameOver else insert a witty dialog    
		}
	}
}
