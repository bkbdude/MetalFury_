using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Hazard))]
[RequireComponent(typeof(Collider))]
public class DamageVolume : MonoBehaviour {

    Hazard hazard;
    public float timer = 0;

    void Start() {
        hazard = GetComponent<Hazard>();
    }
    void Update(){
        if(timer >= 0 ){
            timer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        BaseCarMove car = c.gameObject.transform.root.gameObject.GetComponentInChildren<BaseCarMove>();
        if(car != null && timer <= 0){
            Debug.Log(c.gameObject.transform.root.gameObject.name);
            hazard.DamageCar(car);
            timer = 0.3f;
        }
    }
    void OnCollisionEnter(Collision c) 
    {
        BaseCarMove car = c.gameObject.transform.root.gameObject.GetComponentInChildren<BaseCarMove>();
        if (car != null && timer <= 0)
        {
            Debug.Log(c.gameObject.transform.root.gameObject.name);
            hazard.DamageCar(car);
            timer = 0.3f;
        }
    }
}
