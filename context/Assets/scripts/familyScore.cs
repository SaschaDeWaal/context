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
		string txt = famInfo.coins.ToString ();
		if (famInfo.coins < 0)
			txt = "dead";
		text.text = txt;
	}
}
