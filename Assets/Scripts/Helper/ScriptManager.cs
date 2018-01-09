using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager {

	private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

	private const string MENU_CANVAS_PATH = "Prefabs/Menu/MenuCanvas";
	private const string GAME_MANAGER = "GameManager";

	static ScriptManager() {
		SceneManager.activeSceneChanged += ResetCache;
	}

	private static void ResetCache(Scene prev, Scene next) {
		cache = new Dictionary<string, GameObject> ();
	}

	public static GameController GameController {
		get {
			return (GameController)GetInstanceByName(GAME_MANAGER).GetComponent<GameController>();
		}
	}

	public static GameObjectPool GameObjectPool {
		get {
			return (GameObjectPool)GetInstanceByName(GAME_MANAGER).GetComponent<GameObjectPool>();
		}
	}

	public static MenuCanvasController MenuCanvasController {
		get {
			return GetPrefabInstanceByPath(MENU_CANVAS_PATH).GetComponent<MenuCanvasController>();
		}
	}


	private static GameObject GetPrefabInstanceByPath(string prefabPath) {
		var prefabName = GetInstanceName (prefabPath);
		var prefabInstance = GetInstanceByNameUnchecked (prefabName);

		if (!prefabInstance) {
			prefabInstance = UnityEngine.MonoBehaviour.Instantiate (Resources.Load (prefabPath)) as GameObject;
			prefabInstance.name = prefabName;
			cache.Add (prefabName, prefabInstance);
		}

		if (!prefabInstance) {
			throw new UnityException (prefabName + " prefab is not found on path = " + prefabPath);
		}
		return prefabInstance;
	}


	private static GameObject GetInstanceByName(string prefabName) {
		var instance = GetInstanceByNameUnchecked (prefabName);
		if (!instance) {
			throw new UnityException (prefabName + " instance is not found");
		}
		return instance;
	}


	private static GameObject GetInstanceByNameUnchecked (string instanceName)
	{
		GameObject instance = null;
		cache.TryGetValue (instanceName, out instance);
		if (instance != null) {
			return instance;
		}
		return GameObject.Find (instanceName);
	}


	private static string GetInstanceName (string prefabPath)
	{
		string[] path = prefabPath.Split ('/');
		return path [path.Length - 1];
	}

}
