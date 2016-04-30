using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
		
	public float xSpeed = 1;
	public float ySpeed = 1;
	public float zSpeed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (xSpeed, ySpeed, zSpeed);


	}
}
