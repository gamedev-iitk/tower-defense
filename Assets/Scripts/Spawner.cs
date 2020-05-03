using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public GameObject bonusEnemy;

    public int bonusWave;

    public float spawnRate;

    public int waveSetSize;

    public int setNumber;

    public float cooldownPeriod;

    public float setPeriod;

    float periodTimer = 0f;

    float setTimer = 0f;
    float spawnTimer = 0f;

    int count = 0;

    int setCount = 0;
    bool toWait = true;

    bool isWaveActive = false;

    bool toSpawn = false;
    bool isOnBonusWave = false;

    string headingText;

    string timerText;

    WaveTimerUI waveTimerUI;
    public Transform[] spawnPoints;

    int spawnIndex;

    public static List<GameObject> enemyList;

    void Start()
    {
        spawnIndex=0;
        headingText = "Cooldown";
        timerText = "Wave In: " + ((int)(cooldownPeriod - periodTimer) + 1).ToString();
        waveTimerUI = GameObject.Find("UIManager/WaveTimerUI").GetComponent<WaveTimerUI>();
        waveTimerUI.SetHeading(headingText);
        waveTimerUI.SetTimer(timerText);
        enemyList=new List<GameObject>();
    }
    void Update()
    {
        if (toWait)
        {
            periodTimer += Time.deltaTime;
            timerText = "Wave In: " + ((int)(cooldownPeriod - periodTimer) + 1).ToString();
            waveTimerUI.SetTimer(timerText);
            if (periodTimer >= cooldownPeriod)
            {
                Debug.Log("Wave Started");
                StartWave();
            }
            else
            {
                return;
            }
        }
        if (!isOnBonusWave)
        {
            if (GameState.WaveNumber % bonusWave == 0 && GameState.WaveNumber > 0)
            {
                Debug.Log("Wave is Bonus");
                isOnBonusWave = true;
                headingText = "Bonus Wave";
                waveTimerUI.SetHeading(headingText);
            }
        }
        if (isWaveActive)
        {
            setTimer += Time.deltaTime;
            if (setTimer >= setPeriod)
            {
                setCount++;
                if (setCount <= setNumber)
                {
                    Debug.Log("New Set");
                    spawnIndex=Random.Range(0, spawnPoints.Length);
                    toSpawn = true;
                }
                setTimer = 0f;
            }
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                if (toSpawn)
                {
                    Spawn();
                    Debug.Log("Spawned");
                }
            }
            if (enemyList.Count==0 && setCount>=setNumber)
            {
                Debug.Log("Wave Ended");
                EndWave();
            }
        }
    }

    void Spawn()
    {
        int randomX = Random.Range(-2, 2);
        int randomZ = Random.Range(-2, 2);
        if (isOnBonusWave)
        {
            enemyList.Add(Instantiate(bonusEnemy, spawnPoints[spawnIndex].position+new Vector3(randomX, 0, randomZ), Quaternion.identity));
        }
        else
        {
            enemyList.Add(Instantiate(enemy, spawnPoints[spawnIndex].position+new Vector3(randomX, 0, randomZ), Quaternion.identity));
        }
        count++;
        if (count == waveSetSize)
        {
            toSpawn = false;
            count = 0;
        }
        spawnTimer = 0f;
    }

    void EndWave()
    {
        setCount = 0;
        setTimer = 0;
        periodTimer = 0f;
        toSpawn = false;
        isWaveActive = false;
        toWait = true;
        isOnBonusWave = false;
        headingText = "Cooldown";
        waveTimerUI.SetHeading(headingText);
    }

    public void StartWave()
    {
        periodTimer = 0f;
        GameState.WaveNumber++;
        Debug.Log(GameState.WaveNumber);
        toWait = false;
        isWaveActive = true;
        headingText = "Wave " + GameState.WaveNumber.ToString();
        waveTimerUI.SetHeading(headingText);
        timerText = " ";
        waveTimerUI.SetTimer(timerText);
        toSpawn = true;
        setCount++;
        spawnIndex=Random.Range(0, spawnPoints.Length);
        spawnTimer=spawnRate;
    }
}
