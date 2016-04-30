using UnityEngine;
using System.Collections;

public class PickUpPad : MonoBehaviour {

	public WeaponState power;
	public float spawnerTime = 3;
	public float spawnerTimer = 3;
	bool waitingToSpawn = false;

    public GameObject[] gameArt;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < gameArt.Length; i++)
        {
            gameArt[i].SetActive(false);
        }

        spawnerTimer = spawnerTime;
        gameArt[(int)power].SetActive(true);
        waitingToSpawn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(waitingToSpawn){
			if(spawnerTimer <= 0){
                spawnerTimer = spawnerTime;
                gameArt[(int)power].SetActive(true);
                waitingToSpawn = false;
			}
			else{
				spawnerTimer -= Time.deltaTime;
			}
		}

	}
	void spawnPickUp(){
	}

    void OnTriggerEnter(Collider c)
    {

        BaseCarMove car = c.gameObject.transform.root.gameObject.GetComponentInChildren<BaseCarMove>();
        if (car != null && !waitingToSpawn)
        {
            car.weaponController.ChangeState(power);
            waitingToSpawn = true;
            gameArt[(int)power].SetActive(false);
        }
    }
}
