using UnityEngine;
using System.Collections;

public class school : action {

	public GameObject sprite;
	public Sprite[] keys;

	private SpriteRenderer sr;
	private bool busy = false;

	const int clicksCount = 5;
	const float powerupTime = 40;

	void Start () {
		sr = sprite.GetComponent<SpriteRenderer> ();
	}
	
	public override void Run(int playerID, float distance, GameObject obj){
		if (!busy && !obj.GetComponent<PlayerInfo>().educated) StartCoroutine (schoolTime(obj, playerID));

	}

	//Set random key dir
	Vector2 randomDirection(Vector2 last){
		
		int dir = Random.Range (0,keys.Length);
		Vector2 ret = Vector2.zero;

		sprite.SetActive (true);
		sr.sprite = keys [dir];

		if (dir == 0) ret.y = 1;
		if (dir == 1) ret.x = 1;
		if (dir == 2) ret.y = -1;
		if (dir == 3) ret.x = -1;

		if (ret == last)
			ret = randomDirection (last);

		return ret;
	}

	Vector2 hitKey(int playerID){
		return new Vector2 (Input.GetAxisRaw("Horizontal Player " + playerID.ToString()), Input.GetAxisRaw("Vertical Player " + playerID.ToString()));
	}

	void stopSchool(GameObject player){
		player.GetComponent<movement>().enabled = true;
		player.GetComponent<playerAction> ().enabled = true;
		sprite.SetActive (false);
		busy = false;
	}

	IEnumerator schoolTime(GameObject player, int id){

		//settings things
		player.GetComponent<movement>().enabled = false;
		player.GetComponent<playerAction> ().enabled = false;
		Vector2 lastDir = Vector2.zero;
		busy = true;

		//loop
		for (int i = 0; i < clicksCount; i++) {

			//create data
			Vector2 unloackKey = randomDirection (lastDir);
			Vector2 key = hitKey (id);

			//wait until a key is presed
			while(key == Vector2.zero){
				key = hitKey (id);
				yield return null;
			}

			//check key
			if (key == unloackKey) {
				
				//wait until no key is pressed
				while(hitKey (id) != Vector2.zero) yield return null;
				lastDir = unloackKey;

			} else {

				//stop school
				stopSchool (player);
				yield break;

			}
		}

		//school done
		stopSchool (player);
		player.GetComponent<PlayerInfo> ().educated = true;

		//power up done
		yield return new WaitForSeconds (powerupTime);
		player.GetComponent<PlayerInfo> ().educated = false;

	}
}
