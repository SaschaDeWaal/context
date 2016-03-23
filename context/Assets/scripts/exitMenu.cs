using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class exitMenu : MonoBehaviour {

	public Sprite hover;
	public Sprite normal;


	private Image render;

	void Start () {
		render = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {

		if (Input.mousePosition.y > Screen.height * 0.25f && Input.mousePosition.y < Screen.height * 0.35f) {
			render.sprite = hover;

			if (Input.GetMouseButtonDown (0)) {
				Application.Quit ();
			}
		} else {
			render.sprite = normal;
		}

	}
}
