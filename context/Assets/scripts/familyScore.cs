using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class familyScore : MonoBehaviour {

	public GameObject fam;

	private family famInfo;
	private Text text;

	void Start () {
		famInfo = fam.GetComponent<family> ();
		text = transform.GetChild (0).GetComponent<Text> ();
	}
	
	void Update () {
		text.text = famInfo.coins.ToString ();
	}
}
