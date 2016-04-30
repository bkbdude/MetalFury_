using UnityEngine;
using System.Collections;

public class TransformFollow : MonoBehaviour {

    public Transform item1;
    public Transform item2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        item1.position = item2.position;
        item1.rotation = item2.rotation;
	}
}
