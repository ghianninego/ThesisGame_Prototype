using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject Player;
	public Camera cam;

	void Update () {
			cam.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, -10);
	}

}