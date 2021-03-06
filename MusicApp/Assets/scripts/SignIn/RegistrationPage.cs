﻿//Contributor: Rubaiyat Rashid, Sacit Gonen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using Firebase.Database;
using ToastPlugin;


//This class has necessary information such as email, range, and name about a user which will be stored to Firebase realtime database
public class User {
	public string FirstName;
	public string LastName;
	public string LowerRange;
	public string HigherRange;
	public double OverallAccuracy;
	public int OverallHits;
	public int OverallPossible;

	public User() {
	}

	//This function is a constructor for User
	public User(string f_name, string l_name) {
		this.FirstName = f_name;
		this.LastName = l_name;
		this.LowerRange = "a3";
		this.HigherRange = "a5";
		this.OverallAccuracy = 100;
		this.OverallHits = 0;
		this.OverallPossible = 0;
	}
}

//Namespace for the sign in and registration authentication code
namespace SignIn{
	//This class 
	public class RegistrationPage : MonoBehaviour {
		Firebase.Auth.FirebaseAuth auth;
		Firebase.Auth.FirebaseUser user;
		public InputField email;
		public InputField password;
		public InputField fname;
		public InputField lname;

		//On start, Firebase is set up. 
		void Start() {
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://music-learning-capstone-c019b.firebaseio.com");
		}

		//When user clicks on register submit, an account of user is created with email and password on firebase. 
		public void RegisterSubmit (string scene_name) {
			FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(obj => {
				if (obj.IsFaulted){
					ToastHelper.ShowToast ("Registration Failed. Try again.", true);
				}
				else if (obj.IsCompleted) {
					auth = Firebase.Auth.FirebaseAuth.GetAuth (FirebaseAuth.DefaultInstance.App);
					user = auth.CurrentUser;
					AddUser(fname.text, lname.text);
					SceneManager.LoadSceneAsync (scene_name);
				}
			});
		}

		//This function adds user to the firebase realtime database
		private void AddUser(string f_name, string l_name) {
			User new_user = new User(f_name, l_name);

			//This returns a reference to a existing table, User Table, from Firebase
			DatabaseReference user_table = FirebaseDatabase.DefaultInstance.GetReference ("User Table");
			if (user != null) {
				string json = JsonUtility.ToJson(new_user);

				//This returns a reference to the child of User Table, the user Id
				user_table = user_table.Child (user.UserId);
				user_table.SetRawJsonValueAsync (json);
			}
		}
	}
}