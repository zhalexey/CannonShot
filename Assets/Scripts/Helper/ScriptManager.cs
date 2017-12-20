using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptManager {

	private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();
	private const string MENU_CANVAS = "MenuCanvas";
	private const string GAME_MANAGER = "GameManager";
	private const string MENU_CANVAS_PATH = "Prefabs/Menu/MenuCanvas";

	static ScriptManager() {
		SceneManager.activeSceneChanged += ResetCache;
	}

	private static void ResetCache(Scene prev, Scene next) {
		cache = new Dictionary<string, GameObject> ();
	}

	public static GameController GameController {
		get {
			return (GameController)GameObject.Find (GAME_MANAGER).GetComponent<GameController>();
		}
	}

	public static MenuCanvasController MenuCanvasController {
		get {
			GameObject prefabInstance = null;
			cache.TryGetValue (MENU_CANVAS, out prefabInstance);
			if (prefabInstance == null) {
				GameObject prefab = Resources.Load (MENU_CANVAS_PATH) as GameObject;
				prefabInstance = MonoBehaviour.Instantiate (prefab);
				cache.Add (MENU_CANVAS, prefab);
			}
			return (MenuCanvasController)prefabInstance.GetComponent<MenuCanvasController>();
		}
	}

}
