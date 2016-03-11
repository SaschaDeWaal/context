using UnityEngine;
using System.Collections;

public class family : action {

	public int player;
	public int coins = 8;

	const float spendTime = 5;
	const float minDis = 1.5f;

	void Start(){
		StartCoroutine (loop());
	}

	IEnumerator loop(){

		//alive
		while(coins > -1){
			yield return new WaitForSeconds (spendTime);
			coins -= 1;
		}

		//dead
		Debug.Log("Fam dead");
	}


	public override void Run(int playerID, float distance, GameObject obj){
		if (playerID == player && distance < minDis) {
			if (obj.GetComponent<PlayerInfo> ().coins > 0) {
				obj.GetComponent<PlayerInfo> ().RemoveCoin (1);
				coins++;
			}
		}
	}
}
