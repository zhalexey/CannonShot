using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvasController: MonoBehaviour {

	public void OnContinue() {
		SceneManager.LoadScene ("scene1");
	}

	public void Activate() {
		gameObject.SetActive (true);
	}
}
