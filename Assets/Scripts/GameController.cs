using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private enum GameState {
		Started, Finished
	}

	public static GameObject root;

	private const float SPAWN_TIME = 1f;
	public const string ENEMY_TAG = "Enemy";
	private const float ENEMY_SPEED = 2f;
	public const string BULLET_TAG = "Bullet";
	public const string EXPLOSION_TAG = "Explosion";
	public const string ENEMY_CRASH_TAG = "EnemyCrash";


	private float currentTime;
	private GameState state;

	void Awake() {
		root = new GameObject ("Root");
	}

	void Start () {
		currentTime = Time.time;
		state = GameState.Started;
	}

	void Update() {
		if (GameState.Started == state) {
			if (IsTimeToSpawn ()) {
				SpawnEnemy ();
				currentTime = Time.time;
			}
		}
	}

	public void StopGame() {
		state = GameState.Finished;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (ENEMY_TAG);
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		}
	}

	public void ActivateMenu() {
		ScriptManager.MenuCanvasController.Activate();
		Cursor.visible = true;
	}

	private void SpawnEnemy (){
		Vector3 position = GetEnemyRandomPosition ();

		float angle = GeomHelper.GetAngle (-position);
		Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);

		GameObject enemy = ScriptManager.GameObjectPool.GetEnemy ();
		enemy.transform.position = position;
		enemy.transform.rotation = rotation;
		enemy.transform.GetComponent<Rigidbody2D>().velocity = -position.normalized * ENEMY_SPEED * Random.Range(0.5f, 1f);
	}

	private Vector3 GetEnemyRandomPosition() {
		Vector3 position = GeomHelper.GetCircleRandomPosition();
		position.z = 0;
		return position;
	}

	private bool IsTimeToSpawn ()
	{
		return Time.time - currentTime >= SPAWN_TIME;
	}

	public bool IsFinished() {
		return GameState.Finished == state;
	}

}
