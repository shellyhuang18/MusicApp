using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class Registration_Page : MonoBehaviour {

	public InputField email;
	public InputField password;
	public InputField fname;
	public InputField lname;

	// Use this for initialization
	void Start () {
		
	}
		
	public void register_submit (string sceneName) {
		FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith((obj) => {
			SceneManager.LoadSceneAsync (sceneName);
			});
	}

	// Update is called once per frame
	void Update () {
		
	}
}
