/* PlayButton.cs
 * Author: Ghiannine Go
 * 
 * This script is used to load the MainGame scene and start the game
 * 
 * */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour {

	/* This function will fire once the Start Game button is clicked.
	 * 
	 * param: none
	 * return: none
	 * */
	void OnClick () {
		SceneManager.LoadScene (1);
	}
}
