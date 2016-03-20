using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class coinAnimation : MonoBehaviour {


	public GameObject cointObj;
	public AudioClip addCoinSound;

	private Vector3 addToPos = new Vector3(0,1.2f,-1);
	private PlayerInfo info;
	private Vector3 payMovePos = Vector3.zero;
	private List<coinData> coinsData = new List<coinData> ();

	const float carrySize = 0.05f;
	const float speed = 5;

	void Start () {
		info = GetComponent<PlayerInfo> ();
	}

	public void removedStone(Vector3 stonePosition){
		GetComponent<AudioSource> ().PlayOneShot (addCoinSound);
		StartCoroutine (coinEffect(stonePosition, new Vector3(info.coins*0.2f,0,info.coins*-0.1f)));
	}

	public void giveGoin(Vector3 toPos, int ammount = 1){
		StartCoroutine (giveCoins(toPos, ammount));
	}

	IEnumerator giveCoins(Vector3 pos, int ammount){
		for (int i = 0; i < ammount; i++) {
			payMovePos = pos;
			yield return new WaitForSeconds (0.3f);
		}
	}

	string genName(){
		string ret = "";
		string[] chars = new string[]{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
		while (ret.Length < 10) {
			ret += chars[Random.Range(0, chars.Length)];
		}

		return ret;
	}

	int findIndex(string name){
		for (int i = 0; i < coinsData.Count; i++) {
			if (coinsData [i].id == name)
				return i;
		}

		return -1;
	}

	IEnumerator coinEffect(Vector3 fron, Vector3 add){
		GameObject coin = Instantiate (cointObj, fron, new Quaternion (0, 0, 0, 0)) as GameObject;
		string name = genName ();
		coinsData.Add (new coinData(name));


		yield return new WaitForSeconds(0.2f);


		while(payMovePos == Vector3.zero){


			int index = findIndex (name);
			Vector3 amountAdd = new Vector3 (coinsData.Count*-0.1f,0,0) +  new Vector3(index*0.2f,0,index*-0.1f);

			coin.transform.position = Vector3.Lerp (coin.transform.position, transform.position+addToPos+amountAdd, Time.deltaTime*speed);
			coin.transform.localScale = Vector3.Lerp (coin.transform.localScale, new Vector3(carrySize, carrySize, carrySize),Time.deltaTime*speed);


			yield return null;
		}

		Vector3 moveTo = payMovePos;
		payMovePos = Vector3.zero;
		coinsData.RemoveAt (findIndex (name));


		while(Vector3.Distance(coin.transform.position, moveTo) > 0.1f){
			coin.transform.position = Vector3.Lerp (coin.transform.position, moveTo, Time.deltaTime*speed);

			yield return true;
		}
			
		yield return new WaitForSeconds (0.1f); 
		Destroy (coin);
	}
}
