using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public GameObject explosionParticle;
	public GameObject enemyCrashParticle;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == GameController.ENEMY_TAG) {
			SoundController.instance.Explode ();

			GameObject crashEffect =  Instantiate (enemyCrashParticle, other.gameObject.transform.position, Quaternion.identity);
			crashEffect.transform.parent = GameController.root.transform;
			ParticleSystem crashEffectPS = crashEffect.GetComponent<ParticleSystem> ();
			crashEffectPS.Play ();
			Destroy (crashEffect, crashEffectPS.main.duration);

			GameObject explosionEffect =  Instantiate (explosionParticle, other.gameObject.transform.position, Quaternion.identity);
			explosionEffect.transform.parent = GameController.root.transform;
			ParticleSystem explosionEffectPS = explosionEffect.GetComponent<ParticleSystem> ();
			explosionEffectPS.Play ();
			Destroy (explosionEffect, explosionEffectPS.main.duration);

			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
