using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

public class Playercontroller : MonoBehaviour {


    public int playerId = 0;
    public Player input;//Player Rewired input
    public bool canInput = true;


    public GameObject cameraObject;

    public GameObject carPrefab;
    public GameObject[] cars;
    public int carsIndex = 0;

    public SpawnPoint startSpawn;
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    //public SpawnPoint[] spawnPoints;

	// Use this for initialization
     void Start () {

        input = ReInput.players.GetPlayer(playerId);
        cameraObject.GetComponent<CameraFollow>().player = this;
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i] = Instantiate(carPrefab, startSpawn.transform.position, Quaternion.identity) as GameObject;
            cars[i].transform.parent = this.transform;
            cars[i].GetComponent<BaseCarMove>().player = this;
            if(i != 0){
                cars[i].SetActive(false);
            }
        }
        cameraObject.GetComponent<CameraFollow>().focusPoint.transform.root.GetComponentInChildren<TransformFollow>().item2 = cars[carsIndex].transform;
	}

    public void Respawn(int killerId) {

        if(GameManager.instance.currentGameMode == GameMode.timed){

            if (killerId >= 0 && killerId != playerId)
            {
                GameManager.instance.playersScore[killerId]++;
                Debug.Log("PlayerId: " + playerId + " was killed by PlayerId: " + killerId);
            }
            else
            {
                GameManager.instance.playersScore[playerId]--;
                Debug.Log("PlayerId: " + playerId + " self kill");
            }

        }
        else if (GameManager.instance.currentGameMode == GameMode.survival)
        {
            GameManager.instance.playersScore[playerId]--;
            if (GameManager.instance.playersScore[playerId] <= 0)
            {
            //This player lost the game
            }
        }


        cameraObject.GetComponent<CameraFollow>().canRot = false;
        cars[carsIndex].SetActive(false);

        if (carsIndex < cars.Length-1)
        {
            carsIndex++;
        }
        else {
            carsIndex = 0;
        }

        cars[carsIndex].SetActive(true);
        cars[carsIndex].GetComponent<BaseCarMove>().Reset();

        cameraObject.GetComponent<CameraFollow>().focusPoint.transform.root.GetComponentInChildren<TransformFollow>().item2 = cars[carsIndex].transform;
        cameraObject.GetComponent<CameraFollow>().canRot = true;

        int rand = Random.Range(0, spawnPoints.Count);
        cars[carsIndex].transform.position = spawnPoints[rand].transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
