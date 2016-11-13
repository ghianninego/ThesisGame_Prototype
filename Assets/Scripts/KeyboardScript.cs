using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class KeyboardScript : MonoBehaviour {


	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			File.WriteAllText ("key.txt", 1.ToString());
		}
	}
}
