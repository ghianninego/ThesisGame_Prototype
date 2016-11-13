using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class KeyboardScript : MonoBehaviour {


	void Update () {


		if (Input.GetKeyDown (KeyCode.Space)) {
			try{
				File.WriteAllText ("Emotion Recognition System/trigger.txt", 1.ToString());
			} catch (System.Exception e){
				Debug.Log ("Some error :");
			}


		}
	}
}
