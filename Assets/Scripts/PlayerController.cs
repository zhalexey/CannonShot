using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	private const float BULLET_FORCE = 5f;
	private const float BULLET_LIFETIME = 3f;
	private const float RUDE_ROTATION_SPEED = 5f;
	private const float PRECISE_ROTATION_SPEED = 0.2f;

	public GameObject crossHairPrefab;
	public GameObject playerCrashParticle;
	public GameObject explosionParticle;

	private float cannonDist;
	private float targetAngle;
	private float currentAngle;
	private float rotationSpeed;

	private GameObject crossHair;

	void Start() {
		Cursor.visible = false;
		var sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
		cannonDist = sprite.rect.width / sprite.pixelsPerUnit / 2;
		currentAngle = 0f;
		crossHair = Instantiate (crossHairPrefab);
	}

	void Update () {
		if (ScriptManager.GameController.IsFinished ()) {
			return;
		}

		HandleInput ();
		PerformRotation ();
		DisplayCrossHair ();
	}

	private void HandleInput() {
		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}
	}

	private void DisplayCrossHair() {
		crossHair.transform.position = GetMousePos ();
	}

	private void Fire() {
		GameObject bullet = ScriptManager.GameObjectPool.GetBullet ();
		bullet.transform.position = GetCannonSidePosition ();
		bullet.transform.GetComponent<Rigidbody2D> ().velocity = GetCannonSidePosition().normalized * BULLET_FORCE;
		StartCoroutine (ScriptManager.GameObjectPool.Destroy(bullet, BULLET_LIFETIME));
		SoundController.instance.Fire ();
	}

	private IEnumerator DestroyBullet(GameObject bullet, float time) {
		yield return new WaitForSeconds (time);
		bullet.SetActive (false);
	}

	private Vector2 GetCannonSidePosition ()
	{
		float angle = currentAngle * Mathf.Deg2Rad;
		return new Vector2 (cannonDist * Mathf.Cos (angle), cannonDist * Mathf.Sin (angle));
	}

	private void PerformRotation ()
	{
		targetAngle = GeomHelper.GetAngle (GetMousePos());
		var delta = targetAngle - currentAngle;

		if (Mathf.Abs(delta) <= PRECISE_ROTATION_SPEED) {
			return;
		}


		rotationSpeed = Mathf.Abs(delta) <= RUDE_ROTATION_SPEED ? PRECISE_ROTATION_SPEED : RUDE_ROTATION_SPEED;


		if (delta > 0) {
			if (delta > 180) {
				currentAngle -= rotationSpeed;
			} else {
				currentAngle += rotationSpeed;
			}

		} else if (delta < 0) {
			if (delta < -180) {
				currentAngle += rotationSpeed;
			} else {
				currentAngle -= rotationSpeed;
			}
			
		}

		if (currentAngle > 360) {
			currentAngle = currentAngle - 360; 
		}

		if (currentAngle < 0) {
			currentAngle = 360 + currentAngle; 
		}

		gameObject.transform.rotation = Quaternion.AngleAxis (currentAngle, Vector3.forward);
	}

	private Vector2 GetMousePos ()
	{
		return (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (GameController.ENEMY_TAG == other.gameObject.tag) {

			ScriptManager.GameController.StopGame ();
			gameObject.GetComponent<SpriteRenderer>().enabled = false;

			SoundController.instance.Explode ();

			GameObject crashffect =  Instantiate (playerCrashParticle, gameObject.transform.position, Quaternion.identity);
			crashffect.GetComponent<ParticleSystem>().Play ();
			crashffect.transform.parent = GameController.root.transform;

			GameObject explosionEffect =  Instantiate (explosionParticle, gameObject.transform.position, Quaternion.identity);
			var explosionEffectParticleSystem = explosionEffect.GetComponent<ParticleSystem> ();
			explosionEffectParticleSystem.Play ();
			explosionEffect.transform.parent = GameController.root.transform;

			StartCoroutine (ActivateMenuDelayed (explosionEffectParticleSystem.main.duration));
		}
	}

	IEnumerator ActivateMenuDelayed(float duration) {
		yield return new WaitForSeconds (duration);
		ScriptManager.GameController.ActivateMenu ();
	}
}
