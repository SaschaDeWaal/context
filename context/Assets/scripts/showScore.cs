using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class showScore : MonoBehaviour {

	public GameObject player;

	private Text text;
	private PlayerInfo info;

	void Start () {
		text = transform.GetChild (0).GetComponent<Text> ();
		info = player.GetComponent<PlayerInfo> ();
	}
	
	void Update () {
		text.text = AddZeros (info.coins);
	}

	string AddZeros(int score){
		string ret = score.ToString ();
		if (ret.Length < 2)
			ret = " " + ret;
		if (score > 99)
			ret = "++";
		return ret;
	}


}
