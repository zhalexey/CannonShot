using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == GameController.ENEMY_TAG) {
			SoundController.instance.Explode ();

			GameObject crashEffect =  ScriptManager.GameObjectPool.GetEnemyCrashParticle();
			crashEffect.transform.position = other.gameObject.transform.position;
			ParticleSystem crashEffectPS = crashEffect.GetComponent<ParticleSystem> ();
			crashEffectPS.Play ();
			StartCoroutine (ScriptManager.GameObjectPool.Destroy(crashEffect, crashEffectPS.main.duration));

			GameObject explosionEffect =  ScriptManager.GameObjectPool.GetExplosionParticle();
			explosionEffect.transform.position = other.gameObject.transform.position;
			ParticleSystem explosionEffectPS = explosionEffect.GetComponent<ParticleSystem> ();
			explosionEffectPS.Play ();
			StartCoroutine (ScriptManager.GameObjectPool.Destroy(explosionEffect, explosionEffectPS.main.duration));

			other.gameObject.SetActive(false);
		}
	}
}
