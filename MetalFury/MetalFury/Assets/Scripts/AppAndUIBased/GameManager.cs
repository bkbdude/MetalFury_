using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameMode {timed,survival,jugernaut,capTheFlag,Tokens}

public class GameManager : MonoBehaviour {

    public GameObject spawnPointDefault;
    public int numberOfPlayers = 1;
    public GameObject[] players;
    public GameObject playerControllerPrefabs;
    SpawnPoint[][] spawnPoints;  

    public GameMode currentGameMode = GameMode.timed;
    public bool matchStart = false;
    float startDelayTimer = 3;


    internal int[] playersScore = new int[4];

    [Header("Timed Mode")]
    public float gameTime = 300;
    public float gameTimer;

    private static GameManager _instance;

    //This is the public reference that other classes will use
    public static GameManager instance
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameManager>();
            return _instance;
        }
    }


    // Use this for initialization
    void Awake()
    {
        gameTimer = gameTime;
        SpawnPlayerControllers();
        //gameTimer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDelayTimer <= 0)
        {
            enablePlayersInput(true);
            matchStart = true;
        }
        else {
            startDelayTimer -= Time.deltaTime;
        }

        if (matchStart)
        {
            if (currentGameMode == GameMode.timed)
            {
                if (gameTimer <= 0)
                {
                    enablePlayersInput(false);
                    GameOver();
                }
                else
                {
                    gameTimer -= Time.deltaTime;
                    //display timer
                    //timerUI.text = SecondsToString((int)gameTimer);
                }
            }
        }
    }

    void enablePlayersInput(bool value) {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].GetComponent<Playercontroller>().canInput = value;
        }
    }



    void GameOver() {
    //stop cars
    //wait dealy time
    //find game mode
    //count score based on game mode
    //move to winner screen
        Debug.Log("GameOver " +"X"+" Wins");
    }

    void SpawnPlayerControllers() {

        SpawnPoint[] allSpawnPoints = FindObjectsOfType<SpawnPoint>();

        List<SpawnPoint> player1 = new List<SpawnPoint>();
        //SpawnPoint[] player1 = new SpawnPoint[0];
        SpawnPoint player1Start = null;
        int player1interator = 0;

        List<SpawnPoint> player2 = new List<SpawnPoint>();
        //SpawnPoint[] player2 = new SpawnPoint[10];
        SpawnPoint player2Start = null;
        int player2interator = 0;

        List<SpawnPoint> player3 = new List<SpawnPoint>();
        //SpawnPoint[] player3 = new SpawnPoint[10];
        SpawnPoint player3Start = null;
        int player3interator = 0;

        List<SpawnPoint> player4 = new List<SpawnPoint>();
        //SpawnPoint[] player4 = new SpawnPoint[10];
        SpawnPoint player4Start = null;
        int player4interator = 0;

        for (int i = 0; i < allSpawnPoints.Length; i++)
        {
            if(allSpawnPoints[i].playerId == 0){
                if (allSpawnPoints[i].matchStartSpawn) {
                    player1Start = allSpawnPoints[i];
                }
                else
                {
                    player1.Add(allSpawnPoints[i]);
                    player1interator++;
                }
            }
            else if (allSpawnPoints[i].playerId == 1)
            {
                if (allSpawnPoints[i].matchStartSpawn)
                {
                    player2Start = allSpawnPoints[i];
                }
                else
                {
                    player2.Add(allSpawnPoints[i]);
                    player2interator++;
                }
            }
            else if (allSpawnPoints[i].playerId == 2)
            {
                if (allSpawnPoints[i].matchStartSpawn)
                {
                    player3Start = allSpawnPoints[i];
                }
                else
                {
                    player3.Add(allSpawnPoints[i]);
                    player3interator++;
                }
            }
            else if (allSpawnPoints[i].playerId == 3)
            {
                if (allSpawnPoints[i].matchStartSpawn)
                {
                    player4Start = allSpawnPoints[i];
                }
                else
                {
                    player4.Add(allSpawnPoints[i]);
                    player4interator++;
                }
            }
        }

        if (numberOfPlayers == 1)
        {
            players[0] = Instantiate(playerControllerPrefabs, Vector3.zero, Quaternion.identity) as GameObject;
            players[0].GetComponent<Playercontroller>().playerId = 0;

            players[0].GetComponent<Playercontroller>().startSpawn = player1Start;
            players[0].GetComponent<Playercontroller>().spawnPoints = player1;
        }
        if (numberOfPlayers == 2)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = Instantiate(playerControllerPrefabs,Vector3.zero,Quaternion.identity) as GameObject;
                players[i].GetComponent<Playercontroller>().playerId = i;
                
                if(i == 0){
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0,0.5f,1,0.5f);
                }else{
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0,0,1,0.5f);
                }
            }

            players[0].GetComponent<Playercontroller>().startSpawn = player1Start;
            players[0].GetComponent<Playercontroller>().spawnPoints = player1;
            players[1].GetComponent<Playercontroller>().startSpawn = player2Start;
            players[1].GetComponent<Playercontroller>().spawnPoints = player2;
        }
        if (numberOfPlayers == 3)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = Instantiate(playerControllerPrefabs, Vector3.zero, Quaternion.identity) as GameObject;
                players[i].GetComponent<Playercontroller>().playerId = i;
                if (i == 0)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (i == 1)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (i == 2)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                }
            }
            players[0].GetComponent<Playercontroller>().startSpawn = player1Start;
            players[0].GetComponent<Playercontroller>().spawnPoints = player1;
            players[1].GetComponent<Playercontroller>().startSpawn = player2Start;
            players[1].GetComponent<Playercontroller>().spawnPoints = player2;
            players[2].GetComponent<Playercontroller>().startSpawn = player3Start;
            players[2].GetComponent<Playercontroller>().spawnPoints = player3;
        }
        if (numberOfPlayers == 4)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i] = Instantiate(playerControllerPrefabs, Vector3.zero, Quaternion.identity) as GameObject;
                players[i].GetComponent<Playercontroller>().playerId = i;
                if (i == 0)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                }
                else if (i == 1)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                }
                else if (i == 2)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                }
                else if (i == 3)
                {
                    players[i].GetComponent<Playercontroller>().cameraObject.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                }
            }

            players[0].GetComponent<Playercontroller>().startSpawn = player1Start;
            players[0].GetComponent<Playercontroller>().spawnPoints = player1;
            players[1].GetComponent<Playercontroller>().startSpawn = player2Start;
            players[1].GetComponent<Playercontroller>().spawnPoints = player2;
            players[2].GetComponent<Playercontroller>().startSpawn = player3Start;
            players[2].GetComponent<Playercontroller>().spawnPoints = player3;
            players[3].GetComponent<Playercontroller>().startSpawn = player4Start;
            players[3].GetComponent<Playercontroller>().spawnPoints = player4;
        }

    }

    public static string SecondsToString(int sec)
    {
        int minutes = sec / 60;
        int seconds = sec % 60;

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
