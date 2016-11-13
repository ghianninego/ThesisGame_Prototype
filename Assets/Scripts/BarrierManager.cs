using UnityEngine;
using System.Collections;

public class BarrierManager : MonoBehaviour {

	public static BarrierManager Singleton = null;

	GameObject happyBarrier;
	GameObject sadBarrier;
	GameObject angerBarrier;
	GameObject fearBarrier;
	GameObject surpriseBarrier;
	GameObject disgustBarrier;

	public UILabel emotionLabel;
	public UISprite emotionSprite;

	void Start () {
		if (Singleton == null) {
			Singleton = this;
		} else {
			Destroy (Singleton);
		}

		happyBarrier = GameObject.FindGameObjectWithTag ("Happy");
		sadBarrier = GameObject.FindGameObjectWithTag ("Sad");
		angerBarrier = GameObject.FindGameObjectWithTag ("Anger");
		fearBarrier = GameObject.FindGameObjectWithTag ("Fear");
		surpriseBarrier = GameObject.FindGameObjectWithTag ("Surprise");
		disgustBarrier = GameObject.FindGameObjectWithTag ("Disgust");

		InitializeBarrier ();
	}

	void InitializeBarrier() {
		happyBarrier .SetActive(true);
		sadBarrier .SetActive (true);
		angerBarrier .SetActive (true);
		fearBarrier .SetActive (true);
		surpriseBarrier .SetActive (true);
		disgustBarrier .SetActive (true);
	}

	public void EmotionFound(string emotion) {
		CloseBarrier (emotion);
		switch(emotion) {
			case "ANGER":
				IsAngry ();
				emotionLabel.text = "ANGER";
				emotionSprite.spriteName = emotion;
				break;
			case "DISGUST":
				IsDisgust ();
				emotionLabel.text = "DISGUST";
				emotionSprite.spriteName = emotion;
				break;
			case "FEAR":
				IsFear ();
				emotionLabel.text = "FEAR";
				emotionSprite.spriteName = emotion;
				break;
			case "HAPPINESS":
				IsHappy ();
				emotionLabel.text = "HAPPY";
				emotionSprite.spriteName = emotion;
				break;
			case "SADNESS":
				IsSad ();
				emotionLabel.text = "SADNESS";
				emotionSprite.spriteName = emotion;
				break;
			case "SURPRISE":
				IsSurprise ();
				emotionLabel.text = "SURPRISE";
				emotionSprite.spriteName = emotion;
				break;
			case "NEUTRAL":
				emotionLabel.text = "NEUTRAL";
				emotionSprite.spriteName = emotion;
				break;
			default:
				//return nothing
				break;
		}

		//GameManager.Instance.playersEmotionMenu.SetActive (true);
	}

	void IsHappy() {
		happyBarrier  .SetActive (false);
	}

	void IsSad() {
		sadBarrier  .SetActive (false);
	}

	void IsAngry() {
		angerBarrier  .SetActive (false);
	}

	void IsFear() {
		fearBarrier  .SetActive (false);
	}

	void IsSurprise() {
		surpriseBarrier .SetActive(false);
	}

	void IsDisgust() {
		disgustBarrier  .SetActive (false);
	}

	void CloseBarrier(string emotion){
		switch(emotion) {
		case "ANGRY":
			happyBarrier.SetActive(true);
			sadBarrier.SetActive (true);
			fearBarrier.SetActive (true);
			surpriseBarrier.SetActive (true);
			disgustBarrier.SetActive (true);
			break;
		case "DISGUST":
			happyBarrier.SetActive(true);
			sadBarrier.SetActive (true);
			angerBarrier.SetActive (true);
			fearBarrier.SetActive (true);
			surpriseBarrier.SetActive (true);
			break;
		case "FEAR":
			happyBarrier  .SetActive(true);
			sadBarrier  .SetActive (true);
			angerBarrier  .SetActive (true);
			//fearBarrier  .SetActive (true);
			surpriseBarrier  .SetActive (true);
			disgustBarrier  .SetActive (true);
			break;
		case "HAPPY":
			//happyBarrier  .SetActive(true);
			sadBarrier  .SetActive (true);
			angerBarrier  .SetActive (true);
			fearBarrier  .SetActive (true);
			surpriseBarrier  .SetActive (true);
			disgustBarrier  .SetActive (true);
			break;
		case "SAD":
			happyBarrier  .SetActive(true);
			//sadBarrier  .SetActive (true);
			angerBarrier  .SetActive (true);
			fearBarrier  .SetActive (true);
			surpriseBarrier  .SetActive (true);
			disgustBarrier  .SetActive (true);
			break;
		case "SURPRISE":
			happyBarrier  .SetActive(true);
			sadBarrier  .SetActive (true);
			angerBarrier  .SetActive (true);
			fearBarrier  .SetActive (true);
			//surpriseBarrier  .SetActive (true);
			disgustBarrier  .SetActive (true);
			break;
		case "NEUTRAL":
			happyBarrier  .SetActive(true);
			sadBarrier  .SetActive (true);
			angerBarrier  .SetActive (true);
			fearBarrier  .SetActive (true);
			surpriseBarrier  .SetActive (true);
			disgustBarrier  .SetActive (true);
			break;
		default:
			//return nothing
			break;
		}

	}


}