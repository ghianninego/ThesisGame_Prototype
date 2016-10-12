/* PlayButton.cs
 * Author: Ghiannine Go
 * 
 * This script is used to pause the game and show the Pause Menu.
 * 
 * */

using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	void OnClick() {
		Time.timeScale = 0;
		GameManager.Instance.pauseMenu.SetActive(true);
	}
}
