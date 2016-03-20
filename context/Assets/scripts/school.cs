using UnityEngine;
using System.Collections;

public class school : action {

	public GameObject sprite;
	public Sprite[] keys;
	public AudioClip goodSound;
	public AudioClip wrongSound;
	public AudioClip powerUpSong;

	private SpriteRenderer sr;
	private bool busy = false;

	const int clicksCount = 5;
	const float powerupTime = 30;
	const int toPay = 3;

	void Start () {
		sr = sprite.GetComponent<SpriteRenderer> ();
	}
	
	public override void Run(int playerID, float distance, GameObject obj){
		if (distance < 1f && !busy && !obj.GetComponent<PlayerInfo>().educated && obj.GetComponent<PlayerInfo>().coins >= toPay) StartCoroutine (schoolTime(obj, playerID));

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
		return new Vector2 (Mathf.Round( Input.GetAxis("Horizontal Player " + playerID.ToString())), Mathf.Round( Input.GetAxis("Vertical Player " + playerID.ToString())));
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
		player.GetComponent<PlayerInfo> ().RemoveCoin (toPay);
		player.GetComponent<coinAnimation> ().giveGoin (transform.position, toPay);
		Vector2 lastDir = Vector2.zero;
		Vector3 startIconPos = sprite.transform.position;
		busy = true;

		//loop
		for (int i = 0; i < clicksCount; i++) {

			//create data
			Vector2 unloackKey = randomDirection (lastDir);
			Vector2 key = hitKey (id);
			float time = 0;

			sprite.transform.position = startIconPos;

			//wait until a key is presed
			while(key == Vector2.zero){
				key = hitKey (id);

				sprite.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f-time);
				sprite.transform.Translate (Vector3.up*Time.deltaTime*0.5f);
				time += Time.deltaTime*0.5f;

				if (time > 1f) {
					GetComponent<AudioSource> ().PlayOneShot (wrongSound);
					stopSchool (player);
					yield break;
				}

				yield return null;
			}

			//check key
			if (key == unloackKey) {
				
				//wait until no key is pressed
				while(hitKey (id) != Vector2.zero) yield return null;
				lastDir = unloackKey;
				GetComponent<AudioSource> ().PlayOneShot (goodSound);

			} else {

				//stop school
				GetComponent<AudioSource> ().PlayOneShot (wrongSound);
				stopSchool (player);
				yield break;

			}
		}

		//school done
		stopSchool (player);
		sprite.transform.position = startIconPos;
		player.GetComponent<PlayerInfo> ().educated = true;
		AudioSource audio = GetComponent<AudioSource> ();
		Camera.main.GetComponent<AudioSource> ().Pause ();
		audio.PlayOneShot (powerUpSong);

		while (audio.isPlaying) {
			yield return null;
		}

		Camera.main.GetComponent<AudioSource> ().Play ();

		//power up done
		yield return new WaitForSeconds (powerupTime);
		player.GetComponent<PlayerInfo> ().educated = false;

	}
}
