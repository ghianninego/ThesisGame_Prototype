using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class SaveData : MonoBehaviour {
	string filePath = "Saved_data.csv";  
	string delimiter = ",";  
	public static SaveData Singleton = null;

 void Start () {
		if(Singleton==null){
			Singleton = this;
		}else{
			Destroy (Singleton);
		}
		if(!File.Exists(filePath)){
			CreateCSV ();
		}

	}

	void CreateCSV() {
		string[][] output = new string[][]{  
			new string[]{"sep=,"},
			new string[]{"PlayerID",
						"StudentNumber",
						"Name",
						"Section",
						"Percentage"}  
		};  

		int length = output.GetLength(0);  
		StringBuilder sb = new StringBuilder();  
		for (int index = 0; index < length; index++) {
			sb.AppendLine (string.Join (delimiter, output [index]));  
		}

		File.AppendAllText(filePath, sb.ToString());                 
	}

	public void AddData(){ 
		File.AppendAllText(filePath, 
			PlayerData.playerID +","+
			PlayerData.studentNumber +","+
			PlayerData.fullName +","+
			PlayerData.section +","+
			PlayerData.percentage.ToString() +"\n"
		);  
	}
}
