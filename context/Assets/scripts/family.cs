using UnityEngine;
using System.Collections;

public class family : action {

	public int player;
	public int coins = 15;
	public GameObject coinObjet;
	public GameObject scoreHud;

	const float spendTime = 9;
	const float minDis = 4f;

	void Start(){
		StartCoroutine (startGame());
	}

	IEnumerator startGame(){
		scoreHud.SetActive (false);
		Vector3 startPos = transform.position;
		float dir = 1f;
		if (player == 2)
			dir = -1;
		transform.position = new Vector3 (startPos.x-(2*dir), startPos.y, startPos.z);

		yield return new WaitForSeconds (1f);

		scoreHud.SetActive (true);

		while (Vector3.Distance(transform.position, startPos) > 0.03f) {
			transform.position = Vector3.Lerp (transform.position, startPos, Time.deltaTime*5f);

			yield return null;
		}


		transform.position = startPos;

		StartCoroutine (loop());
	}

	IEnumerator loop(){

		//alive
		while(coins > -1){
			yield return new WaitForSeconds (spendTime);
			coins -= 1;
			StartCoroutine (coinAnimation());
		}

		//dead
		GameObject.FindGameObjectWithTag("canvas").GetComponent<endGame>().setWinner(player);
	}

	IEnumerator coinAnimation(){
		
		GameObject coin = Instantiate (coinObjet) as GameObject;
		coin.transform.position = transform.position;
		coin.transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);

		float time = -0.5f;
		SpriteRenderer render = coin.GetComponent<SpriteRenderer> ();

		while(time < 1f){
			time += Time.deltaTime;
			coin.transform.position += new Vector3 (0,0.5f,0)*Time.deltaTime;

			render.color = new Color (1f, 1f, 1f, 1f- time);


			yield return null;
		}

		Destroy (coin);

	}


	public override void Run(int playerID, float distance, GameObject obj){
		if (playerID == player && distance < minDis) {
			if (obj.GetComponent<PlayerInfo> ().coins > 0) {
				obj.GetComponent<PlayerInfo> ().RemoveCoin (1);
				obj.GetComponent<coinAnimation> ().giveGoin (transform.position);
				GetComponent<AudioSource> ().Play ();
				coins++;
			}
		}
	}
}
