using UnityEngine;
using System.Collections;

public class BoostPad : MonoBehaviour {


    public Vector3 direction;
    public float boostForce;
    float timer = 0;

    void OnTriggerEnter(Collider c)
    {
        BaseCarMove car = c.gameObject.transform.root.gameObject.GetComponentInChildren<BaseCarMove>();
        if (car != null && timer <= 0)
        {
            car.rigidbody.AddForce(direction * (boostForce * 10000));
            timer = 0.3f;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
	}
}
