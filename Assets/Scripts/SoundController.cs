using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public static SoundController instance;

	public AudioClip cannonEffect;
	public AudioClip explosionEffect;

	private AudioSource gameSoundSource;


	void Awake() {
		if (!instance) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}
	}

	void Start() {
		gameSoundSource = gameObject.AddComponent<AudioSource> ();
	}

	public void Fire() {
		gameSoundSource.PlayOneShot (cannonEffect);
	}

	public void Explode() {
		gameSoundSource.PlayOneShot (explosionEffect);
	}

}
