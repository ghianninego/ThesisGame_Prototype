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
		for (int i = 0; i < 10; i++) {
			AddData (i, 10f);
		}

	}

	void CreateCSV() {
		string[][] output = new string[][]{  
			new string[]{"sep=,"},
			new string[]{"PlayerID", "Percentage"}  
		};  
		int length = output.GetLength(0);  
		StringBuilder sb = new StringBuilder();  
		for (int index = 0; index < length; index++) {
			sb.AppendLine (string.Join (delimiter, output [index]));  
		}

		File.AppendAllText(filePath, sb.ToString());                 
	}

	public void AddData(int ID , float Percent){
		string output = ID.ToString () + "," + Percent.ToString ()+"\n"; 
		File.AppendAllText(filePath, output);  
	}
}
