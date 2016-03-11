using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public bool educated = false;
	public int coins = 0;

	public void AddCoin(int ad){
		coins += ad;
	}

	public void RemoveCoin(int ad){
		coins -= ad;
	}
}
