using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuButton : MonoBehaviour {

	public Sprite hover;
	public Sprite normal;


	private Image render;

	void Start () {
		render = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.mousePosition.y > Screen.height * 0.35f && Input.mousePosition.y < Screen.height * 0.45f) {
			render.sprite = hover;

			if (Input.GetMouseButtonDown (0)) {
				Scene scene = SceneManager.GetActiveScene(); 
				SceneManager.LoadScene("game");
			}
		} else {
			render.sprite = normal;
		}
	
	}


}
