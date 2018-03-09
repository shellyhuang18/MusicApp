using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class Entry_page : MonoBehaviour {

	public InputField email;
	public InputField password;

	// Use this for initialization
	void Start () {
		
	}

	public void login_submit (string sceneName) {
		FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync (email.text, password.text).ContinueWith((obj) => {
			SceneManager.LoadScene (sceneName);
		});
	}

	public void register_page (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	// Update is called once per frame
	void Update () {

	}
}
