using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{

	public GameObject bulletPrefab;
	public GameObject enemyPrefab;
	public GameObject explosionParticle;
	public GameObject enemyCrashParticle;

	public List<GameObject> objectPool;

	void Start ()
	{
		objectPool = new List<GameObject> ();
	}

	public GameObject Get (string tagName)
	{
		foreach (GameObject obj in objectPool) {
			if (!obj.activeInHierarchy && obj.tag == tagName) {
				obj.SetActive (true);
				return obj;
			}
		}	
		return CreateByTag (tagName);
	}

	private GameObject CreateByTag (string tagName)
	{
		if (GameController.BULLET_TAG == tagName) {
			return CreateBullet ();
		} else if (GameController.ENEMY_TAG == tagName) {
			return CreateEnemy ();
		} else if (GameController.EXPLOSION_TAG == tagName) {
			return CreateExplosionParticle ();
		} else if (GameController.ENEMY_CRASH_TAG == tagName) {
			return CreateEnemyCrashParticle ();
		}
		return null;
	}

	public GameObject GetBullet ()
	{
		return Get (GameController.BULLET_TAG);
	}

	public GameObject GetEnemy ()
	{
		return Get (GameController.ENEMY_TAG);
	}

	public GameObject GetExplosionParticle ()
	{
		return Get (GameController.EXPLOSION_TAG);
	}

	public GameObject GetEnemyCrashParticle ()
	{
		return Get (GameController.ENEMY_CRASH_TAG);
	}

	private GameObject Create (GameObject prefab)
	{
		GameObject instance = Instantiate (prefab);
		instance.SetActive (true);
		instance.transform.parent = GameController.root.transform;
		objectPool.Add (instance);
		return instance;
	}

	private GameObject CreateBullet ()
	{
		return Create (bulletPrefab);
	}

	private GameObject CreateEnemy ()
	{
		return Create (enemyPrefab);
	}

	private GameObject CreateExplosionParticle ()
	{
		return Create (explosionParticle);
	}

	private GameObject CreateEnemyCrashParticle ()
	{
		return Create (enemyCrashParticle);
	}

	public IEnumerator Destroy(GameObject obj, float time) {
		yield return new WaitForSeconds (time);
		obj.SetActive (false);
	}

}
