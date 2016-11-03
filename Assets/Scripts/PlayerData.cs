using UnityEngine;
using System.Collections;

public class PlayerData{
	public static PlayerData instance = null;
	public string studentNumber{ get; set;}
	public string fullName{ get; set;}
	public string section{ get; set;}
	public string playerID{ get; set;}
	public float percentage{ get; set;}

	public PlayerData(
		string _studenNumber,
		string _fullName,
		string _section,
		string _playerID
	){
		if (instance == null) {
			instance = this;
		} else {
			instance = null;
		}

		studentNumber = _studenNumber;
		fullName = _fullName;
		section = _section;
		playerID = _playerID;
		percentage = 0;
	}
		
}
