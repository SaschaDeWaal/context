using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	private bool _educated = false;
	public int coins = 0;
	public AudioClip sound;

	public void AddCoin(int ad){
		coins += ad;
		GetComponent<AudioSource> ().PlayOneShot (sound);
	}

	public void RemoveCoin(int ad){
		coins -= ad;
	}

	public bool educated{
		get{
			return _educated;
		}
		set{
			_educated = value;
			transform.GetChild (0).gameObject.SetActive (_educated);

		}
	}
}
