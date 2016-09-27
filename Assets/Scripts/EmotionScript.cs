using UnityEngine;
using System.Collections;

public class EmotionScript : MonoBehaviour {

	public GameObject[] emotionsGameObject;
	public GameObject[] joy;
	public GameObject[] sad;
	public GameObject[] anger;
	public GameObject[] fear;
	public GameObject[] surprise;
	public GameObject[] disgust;


	void Start () {
		emotionsGameObject = GameObject.FindGameObjectsWithTag("Wall");
		StartCoroutine (SetEmotion());
	}

	void Update () {
		
	}

	void SetAllToFalse(){
		for (int i = 0; i < joy.Length; i++) {
			joy [i].SetActive (false);
		}
		for (int i = 0; i < sad.Length; i++) {
			sad [i].SetActive (false);
		}
		for (int i = 0; i < anger.Length; i++) {
			anger [i].SetActive (false);
		}
		for (int i = 0; i < surprise.Length; i++) {
			surprise [i].SetActive (false);
		}
		for (int i = 0; i < fear.Length; i++) {
			fear [i].SetActive (false);
		}
		for (int i = 0; i < disgust.Length; i++) {
			disgust [i].SetActive (false);
		}

	}

	IEnumerator SetEmotion(){
		while (true) {
			switch (Random.Range (0, 7)) {
			case 0:
				SetAllToFalse ();
				for (int i = 0; i < joy.Length; i++) {
					joy [i].SetActive (true);
				}
				break;

			case 1:
				SetAllToFalse ();
				for (int i = 0; i < sad.Length; i++) {
					sad [i].SetActive (true);
				}
				break;

			case 2:
				SetAllToFalse ();
				for (int i = 0; i < anger.Length; i++) {
					anger [i].SetActive (true);
				}
				break;

			case 3:
				SetAllToFalse ();
				for (int i = 0; i < surprise.Length; i++) {
					surprise [i].SetActive (true);
				}
				break;

			case 4:
				SetAllToFalse ();
				for (int i = 0; i < fear.Length; i++) {
					fear [i].SetActive (true);
				}
				break;

			case 5:
				SetAllToFalse ();
				for (int i = 0; i < disgust.Length; i++) {
					disgust [i].SetActive (true);
				}
				break;

			default:
				break;
			}
			yield return new WaitForSecondsRealtime (10f);
		}
	}
}
