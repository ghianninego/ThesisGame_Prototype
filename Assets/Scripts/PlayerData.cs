using UnityEngine;
using System.Collections;

public class PlayerData{
	public static string studentNumber{ get; set;}
	public static string fullName{ get; set;}
	public static string section{ get; set;}
	public static string playerID{ get; set;}
	public static float percentage{ get; set;}

	public static void SetPlayerData(
		string _studenNumber,
		string _fullName,
		string _section,
		string _playerID
	){

		studentNumber = _studenNumber;
		fullName = _fullName;
		section = _section;
		playerID = _playerID;
		percentage = 0;
	}
		
}
