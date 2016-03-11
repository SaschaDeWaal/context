using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	public int playerIndex;

	public Sprite back;
	public Sprite front;
	public Sprite right;
	public Sprite left;

	private Vector3 size;
	private Vector2 lastDir = new Vector2 (0, 0);
	private Vector2 blockDir = new Vector2 (0, 0);
	private SpriteRenderer sr;
	private GameObject lastHitObj = null;

	const float speed = 4;


	void Start () {
		size = transform.localScale;
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	void move(){

		//get input
		Vector3 dir = new Vector3 (Input.GetAxis("Horizontal Player " + playerIndex.ToString()), Input.GetAxis("Vertical Player " + playerIndex.ToString()));
		lastDir = new Vector3 (Input.GetAxisRaw("Horizontal Player " + playerIndex.ToString()), Input.GetAxisRaw("Vertical Player " + playerIndex.ToString()));

		//check ob still exsist
		if (lastHitObj != null && lastHitObj.transform.position == new Vector3 (10000,100000,10000)) blockDir = Vector2.zero;

		//check allowed to move
		if ((blockDir.x == 0f || (lastDir.x != blockDir.x && (lastDir.x != 0 || lastDir.y != 0 ))) && (blockDir.y == 0f || (lastDir.y != blockDir.y && (lastDir.x != 0 || lastDir.y != 0 )))){

			float reduce = Vector3.Distance (new Vector3(0,0,0), dir);
			float despSize = transform.position.y/100f;

			if (reduce != 0) {
				reduce -= 1f;
				reduce = reduce / 2;
			}

			//motion
			transform.Translate (new Vector3(dir.x*Time.deltaTime*speed, dir.y*Time.deltaTime*speed)*(1f-reduce));
			transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -8, 8), Mathf.Clamp(transform.position.y, -4.4f, 4.4f), transform.position.y-0.5f);
			transform.localScale = new Vector3 (size.x-despSize, size.y-despSize, size.z-despSize);//(transform.position.y);

			//reset block
			blockDir = new Vector2 (0, 0);

			//sprite
			if (lastDir.y == 1) sr.sprite = back;
			if (lastDir.y == -1) sr.sprite = front;
			if (lastDir.x == 1) sr.sprite = right;
			if (lastDir.x == -1) sr.sprite = left;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		blockDir = lastDir;
		lastHitObj = col.transform.gameObject;

	}
}
