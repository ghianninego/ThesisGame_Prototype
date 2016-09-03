using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

	void OnClick() {
		Time.timeScale = 0;
		GameManager.Instance.pauseMenu.SetActive(true);
	}
}
