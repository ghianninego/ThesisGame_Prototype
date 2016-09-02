using UnityEngine;
using System.Collections;

public class Things : MonoBehaviour{

	public static GameObject[] thingsToGet;
	void Awake(){
		thingsToGet = GameObject.FindGameObjectsWithTag ("Objects");
	}

	/**
	void Update(){
		for (int i = 0; i < thingsToGet.Length; i++) {
			Debug.Log (thingsToGet[i].name);
		}
	}
	**/
}
