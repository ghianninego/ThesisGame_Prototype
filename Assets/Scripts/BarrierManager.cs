using UnityEngine;
using System.Collections;

public class BarrierManager : MonoBehaviour {

	public static BarrierManager Instance = null;

	GameObject[] happyBarrier;
	GameObject[] sadBarrier;
	GameObject[] angerBarrier;
	GameObject[] fearBarrier;
	GameObject[] surpriseBarrier;
	GameObject[] disgustBarrier;

	void Start () {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (Instance);
		}

		happyBarrier = GameObject.FindGameObjectsWithTag ("Happy");
		sadBarrier = GameObject.FindGameObjectsWithTag ("Sad");
		angerBarrier = GameObject.FindGameObjectsWithTag ("Anger");
		fearBarrier = GameObject.FindGameObjectsWithTag ("Fear");
		surpriseBarrier = GameObject.FindGameObjectsWithTag ("Surprise");
		disgustBarrier = GameObject.FindGameObjectsWithTag ("Disgust");
	}

	void Update () {
	
	}
}