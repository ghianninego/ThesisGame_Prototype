/* PlayButton.cs
 * Author: Ghiannine Go
 * 
 * This script is used to resume the game.
 * 
 * */

using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour {

	void OnClick(){
		Time.timeScale = 1;
		GameManager.Instance.pauseMenu.SetActive (false);
	}
}
