using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

	private bool loadScene = false;
	private bool activatedHatch = false;
	private int scene;
	public GameObject loadingObject;
	private Text loadingText;

    public void LoadScene(int level){
		activatedHatch = true;
		scene = level;
		loadingText = loadingObject.transform.GetChild(0).GetComponent<Text> ();
	}
		
	void Update() {
		if (activatedHatch && loadScene == false) {
			loadScene = true;
			loadingObject.SetActive (true);
			loadingText.text = "Loading hatch " + scene;

			StartCoroutine(LoadNewScene());
		}

		// If the new scene has started loading...
		if (loadScene == true) {
			// ...pulse the transparency of the loading text
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
		}
	}


	IEnumerator LoadNewScene() {

		// Waits for 3 seconds before executing the next line in the coroutine.
		// Is only necessary for testing. Scenes are so simple that they load too fast to read the "Loading..." text.
		yield return new WaitForSeconds(3);

		// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
		AsyncOperation async = Application.LoadLevelAsync(scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone) {
			yield return null;
		}
	}
}
