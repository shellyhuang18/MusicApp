﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataAnalytics;

namespace PitchLine{
	//The script is used to check if the arrow is interacting with a MusicalNote.
	public class DetectNote : MonoBehaviour {

		//Colliders of the arrow and line gameobjects
		Collider2D arrow_collider;
		Collider2D line_collider;

		private bool detection_enabled; //A bool to check if you want to detect notes

		//private int hit = 0;
		//private int miss = 0;
		private DataAnalysis data;



		// Use this for initialization
		void Start () {
			data = new DataAnalysis (); //initialization of DataAnalysis
			enableDetection ();
			arrow_collider = (Collider2D)GameObject.Find ("arrow").GetComponent<Collider2D>();
			line_collider = (Collider2D)GameObject.Find ("pitch_line").GetComponent<BoxCollider2D> ();
		}

		void FixedUpdate(){
			checkOnPitch ();
			if (Input.GetKeyDown ("g")) {
				data.SetCurrentValues ();
			}
		}


			

	//Write what you want specifically to happen when there is a hit or miss here in this zone
	//=============================================================================
		private void onHit(){
			data.IncrementHits();
			gameObject.GetComponent<ScoreBoard>().IncrementScore(1); //one note hit, hits is incremented
			//gameObject.GetComponent<ScoreBoard> ().PercentageScore (total); //get total from song object
			//gameObject.GetComponent<ScoreBoard> ().Progress (data.GetHits + data.GetMisses, total);  //total = how many  total hits possible 
			//data.updateCurrNote();
			Debug.Log ("hit");

		}

		private void onMiss(){
			data.IncrementMisses (); //on note miss, misses is incremented

			Debug.Log ("miss");
		}

		private void onNothing(){
		}

		private void onComplete(){
//			data.SetCurrentValues ();
			//data.SetCurrentValues (hit, hit + miss);
			//data.CalculateOverall ();
		}
			
	//=============================================================================
			

		private void checkOnPitch(){
			if (detection_enabled) {
				if (!isLineTouchingNote ()) {
					onNothing ();
				} else {
					if (isArrowTouchingNote ()) {
						onHit ();

					} else {
						onMiss ();
					}

				}
			} 
		}

		//Checks if the line's collider is touching any of the note's collider
		private bool isLineTouchingNote(){
			GameObject[] spawned_notes = GameObject.FindGameObjectsWithTag ("MusicalNote");
			foreach (GameObject note in spawned_notes) {
				if (line_collider.IsTouching (note.GetComponent<BoxCollider2D> ())){
					return true;
				}
			}

			// If the loop didn't find break yet, then it must be a miss.

			return false;
		}

		//Checks if the arrow'collider is touching any of the note's collider
		private bool isArrowTouchingNote(){
			//Checks if the arrow is touching atleast one of the spawned notes

			GameObject[] spawned_notes = GameObject.FindGameObjectsWithTag ("MusicalNote");
			foreach (GameObject note in spawned_notes) {
				BoxCollider2D note_collider = note.GetComponent<BoxCollider2D> ();
				if (arrow_collider.IsTouching (note_collider) && line_collider.IsTouching(note_collider)) {
					return true;
				}
			}
			// If the loop didn't find break yet, then it must be a miss.
			return false;

		}
			
		public void enableDetection(){
			this.detection_enabled = true;
		}

		public void disableDetection(){
			this.detection_enabled = false;
		}


	}
}
