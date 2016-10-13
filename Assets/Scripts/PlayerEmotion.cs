﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEmotion : MonoBehaviour {

	public Stack<int> emotionPlayer;
	public Stack<int> emotionComputer;
	void Start () {
		emotionPlayer = new Stack<int> ();
		emotionComputer = new Stack<int> ();
	}

	public int getEmotionFromComputer(){
		return 0;
	}
	
	public void Happy(){
		emotionPlayer.Push (1);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Sad(){
		emotionPlayer.Push (2);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Anger(){
		emotionPlayer.Push (3);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Disgust(){
		emotionPlayer.Push (4);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Surprise(){
		emotionPlayer.Push (5);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Fear(){
		emotionPlayer.Push (6);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public void Neutral(){
		emotionPlayer.Push (0);
		emotionComputer.Push(getEmotionFromComputer ());
	}

	public float computePercent(){
		int same = 0;
		int count = 0;
		while(emotionPlayer.Peek()!=0 && emotionComputer.Peek()!=0){
			count++;
			if (emotionPlayer.Pop () == emotionComputer.Pop()) {
				same++;
			}
		}

		return (same / count) * 100f;
	}
}
