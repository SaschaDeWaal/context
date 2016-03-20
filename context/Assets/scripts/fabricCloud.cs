using UnityEngine;
using System.Collections;

public class fabricCloud : MonoBehaviour {

	public Vector3 startPos;

	const float speed = 0.02f;
	private Vector3 direction = new Vector3 (5f, 1f, 0);

	void Start () {
		transform.localScale = new Vector3 (Random.Range (0.1f, 0.5f), transform.localScale.y, transform.localScale.z);

		if (transform.position.y < 5.1f) {
			Instantiate (gameObject, transform.position+(direction*0.03f), transform.rotation);

		}

	}
	

	void Update () {
		transform.position += direction * Time.deltaTime * speed;

		if (transform.position.y > 5.1f) {
			transform.position = startPos;
			transform.localScale = new Vector3 (Random.Range (0.2f, 0.3f), transform.localScale.y, transform.localScale.z);
		}
	}


}
