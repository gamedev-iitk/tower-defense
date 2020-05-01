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

    public float wavePeriod;

    float setPeriod;

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
    void Start()
    {
        setPeriod = wavePeriod / setNumber;
        headingText = "Cooldown";
        timerText = "Wave In: " + ((int)(cooldownPeriod - periodTimer) + 1).ToString();
        waveTimerUI = GameObject.Find("UIManager/WaveTimerUI").GetComponent<WaveTimerUI>();
        waveTimerUI.SetHeading(headingText);
        waveTimerUI.SetTimer(timerText);
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
            if (GameState.waveNumber % bonusWave == 0 && GameState.waveNumber > 0)
            {
                Debug.Log("Wave is Bonus");
                isOnBonusWave = true;
                headingText = "Bonus Wave";
                waveTimerUI.SetHeading(headingText);
            }
        }
        if (isWaveActive)
        {
            periodTimer += Time.deltaTime;
            timerText = "Cooldown In: " + ((int)(wavePeriod - periodTimer) + 1).ToString();
            waveTimerUI.SetTimer(timerText);
            setTimer += Time.deltaTime;
            if (setTimer >= setPeriod)
            {
                setCount++;
                if(setCount<=setNumber)
                {
                    toSpawn = true;
                }
                setTimer = 0f;
            }
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate )
            {
                if (toSpawn)
                {
                    Spawn();
                }
            }
            if (periodTimer >= wavePeriod)
            {
                Debug.Log("Wave Ended");
                EndWave();
            }
        }
    }

    void Spawn()
    {
        int randomX = Random.Range(0, 10);
        int randomZ = Random.Range(0, 10);
        if (isOnBonusWave)
        {
            Instantiate(bonusEnemy, new Vector3(randomX, 0, randomZ), Quaternion.identity);
        }
        else
        {
            Instantiate(enemy, new Vector3(randomX, 0, randomZ), Quaternion.identity);
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
        toSpawn=false;
        isWaveActive = false;
        toWait = true;
        isOnBonusWave = false;
        headingText = "Cooldown";
        waveTimerUI.SetHeading(headingText);
    }

    void StartWave()
    {
        periodTimer = 0f;
        GameState.waveNumber++;
        Debug.Log(GameState.waveNumber);
        toWait = false;
        isWaveActive = true;
        headingText = "Wave " + (GameState.waveNumber).ToString();
        waveTimerUI.SetHeading(headingText);
        toSpawn = true;
        setCount++;
    }


}
