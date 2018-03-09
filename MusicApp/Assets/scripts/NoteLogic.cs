﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLogic: MonoBehaviour {
	
	public class Chord : Sound{

	}

	public class Note : Sound{
		string pitch;
		int octave;

	}

	public class Sound{
		int duration;
	}

	public class Song{
		//time signature
		float time_sig;
		float tempo;
		Sound[] score = new Sound[0];
	}




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}