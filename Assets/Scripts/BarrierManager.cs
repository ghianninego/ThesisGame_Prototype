﻿using UnityEngine;
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

		InitializeBarrier ();
	}

	void InitializeBarrier() {
		happyBarrier [0].SetActive(true);
		sadBarrier [0].SetActive (true);
		angerBarrier [0].SetActive (true);
		fearBarrier [0].SetActive (true);
		surpriseBarrier [0].SetActive (true);
		disgustBarrier [0].SetActive (true);

		happyBarrier [1].SetActive (false);
		sadBarrier [1].SetActive (false);
		angerBarrier [1].SetActive (false);
		fearBarrier [1].SetActive (false);
		surpriseBarrier [1].SetActive (false);
		disgustBarrier [1].SetActive (false);
	}

	public void EmotionFound(int emotion) {
		switch(emotion) {
			case 1:
				IsHappy ();
				break;
			case 2:
				IsSad ();
				break;
			case 3:
				IsAngry ();
				break;
			case 4:
				IsFear ();
				break;
			case 5:
				IsSurprise ();
				break;
			case 6:
				IsDisgust ();
				break;
			case 0:
			default:
				//return nothing
				break;
		}

		GameManager.Instance.playersEmotionMenu.SetActive (true);
	}

	void IsHappy() {
		//push happy to stack

		if (happyBarrier [0].activeSelf == true) {
			happyBarrier [0].SetActive (false);
			happyBarrier [1].SetActive (true);
		} else if (happyBarrier [1].activeSelf == true) {
			happyBarrier [1].SetActive (false);
			happyBarrier [0].SetActive (true);
		}
	}

	void IsSad() {
		//push sad to stack

		if (sadBarrier [0].activeSelf == true) {
			sadBarrier [0].SetActive (false);
			sadBarrier [1].SetActive (true);
		} else if (sadBarrier [1].activeSelf == true) {
			sadBarrier [1].SetActive (false);
			sadBarrier [0].SetActive (true);
		}
	}

	void IsAngry() {
		//push angry to stack

		if (angerBarrier [0].activeSelf == true) {
			angerBarrier [0].SetActive (false);
			angerBarrier [1].SetActive (true);
		} else if (angerBarrier [1].activeSelf == true) {
			angerBarrier [1].SetActive (false);
			angerBarrier [0].SetActive (true);
		}
	}

	void IsFear() {
		//push fear to stack

		if (fearBarrier [0].activeSelf == true) {
			fearBarrier [0].SetActive (false);
			fearBarrier [1].SetActive (true);
		} else if (fearBarrier [1].activeSelf == true) {
			fearBarrier [1].SetActive (false);
			fearBarrier [0].SetActive (true);
		}
	}

	void IsSurprise() {
		//push surprise to stack

		if (surpriseBarrier [0].activeSelf == true) {
			surpriseBarrier [0].SetActive (false);
			surpriseBarrier [1].SetActive (true);
		} else if (surpriseBarrier [1].activeSelf == true) {
			surpriseBarrier [1].SetActive (false);
			surpriseBarrier [0].SetActive (true);
		}
	}

	void IsDisgust() {
		//push disgust to stack

		if (disgustBarrier [0].activeSelf == true) {
			disgustBarrier [0].SetActive (false);
			disgustBarrier [1].SetActive (true);
		} else if (disgustBarrier [1].activeSelf == true) {
			disgustBarrier [1].SetActive (false);
			disgustBarrier [0].SetActive (true);
		}
	}
}