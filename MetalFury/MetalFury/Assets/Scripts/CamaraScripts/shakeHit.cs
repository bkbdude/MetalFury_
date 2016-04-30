using UnityEngine;
using System.Collections;

public class shakeHit : MonoBehaviour {
	public Vector3 posInf = new Vector3(0.25f, 0.25f, 0.25f);
	public Vector3 rotInf = new Vector3(1, 1, 1);
	public float magn = 1, rough = 1, fadeIn = 0.1f, fadeOut = 2f;
	public float time = 0.5f;
	float timer = 0.5f;

	public bool shake = false;

	// Use this for initialization
	void Start () {
		/*CameraShakeInstance c = GetComponent<CameraShaker>().ShakeOnce(magn, rough, fadeIn, fadeOut);
		c.PositionInfluence = posInf;
		c.RotationInfluence = rotInf;*/
	}
	
	// Update is called once per frame
	void Update () {
		if(/*Input.GetKey(KeyCode.Z)||*/ shake){
			if(timer <= 0){
				shake = false;
			}
			else{
				timer -= Time.deltaTime;
			CameraShakeInstance c = GetComponent<CameraShaker>().ShakeOnce(magn, rough, fadeIn, fadeOut);
			c.PositionInfluence = posInf;
				c.RotationInfluence = rotInf;
			}
		}
	}

	public void Shake(){
		shake = true;
		timer = time;
		CameraShakeInstance c = GetComponent<CameraShaker>().ShakeOnce(magn, rough, fadeIn, fadeOut);
		c.PositionInfluence = posInf;
		c.RotationInfluence = rotInf;
	}
}
