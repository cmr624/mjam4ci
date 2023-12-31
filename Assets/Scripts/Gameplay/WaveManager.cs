using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[Serializable]
public class EnemySpawnInfo
{
    public string ID;
    public float Time;
    public string SpawnPointName;
    public Vector3 Position;
    public float RepeatTime;
    public int NumberToSpawn;
}

[Serializable]
public class Wave
{
    public string Name;
    public string Description;
    //public float Length; //,_ for now, go until all enemimes destroyed
    public EnemySpawnInfo[] Enemies;
}

[Serializable]
public class AllWaves
{
    public Wave[] Waves;
}

public class WaveManager : MonoBehaviour
{
    [Serializable]
    private struct PoolID
    {
        public string ID;
        public MMObjectPooler Pool;
    }

    [Serializable]
    private class SpawnPoint
    {
        public string Name;
        public Vector3 Position;
    }

    [Tooltip("Relative to the persistent data path")]
    [SerializeField]
    private string m_filePath = "Waves.json";
    [SerializeField]
    private PoolID[] m_objectPools;
    [SerializeField]
    private SpawnPoint[] m_spawnPoints;
    [SerializeField]
    private AllWaves m_fallbackWaves;

    //TEMP
    [SerializeField]
    private KeyCode m_startNextWaveKey;
    [SerializeField]
    private KeyCode m_endWaveKey = KeyCode.F3;
    //end TEMP

    private int m_currentWave;
    private AllWaves m_waves;
    private Coroutine m_waveCoroutine;

    public Topic<Wave> CurrentWave { get; } = new Topic<Wave>();
    public Topic<List<EnemySpawnInfo>> ToSpawn { get; } = new Topic<List<EnemySpawnInfo>>(new List<EnemySpawnInfo>());
    public Topic<List<Health>> EnemiesRemaining { get; } = new Topic<List<Health>>(new List<Health>());

    public bool WaveRunning => CurrentWave.Value != null;

    private string FilePath => $"{Application.persistentDataPath}/{m_filePath}";

    private void Awake()
    {
        if (File.Exists(FilePath))
        {
            string fileContents = File.ReadAllText(FilePath);

            m_waves = JsonUtility.FromJson<AllWaves>(fileContents);

            Debug.Log($"Loaded waves from {Application.persistentDataPath}/{m_filePath}");
        }
        else
        {
            m_waves = m_fallbackWaves;

            Debug.Log("Loaded waves from fallback");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_startNextWaveKey))
        {
            StartNextWave();
        }

        if (Input.GetKeyDown(m_endWaveKey))
        {
            EndWave();
        }
    }

    public void StartNextWave()
    {
        if (StartWave(m_currentWave))
        {
            m_currentWave++;
        }
    }

    public bool StartWave(int index)
    {
        if(m_waves.Waves.Length <= index)
        {
            Debug.LogError($"Cannot start wave {index} as there is only data for {m_waves.Waves.Length} waves");
            return false;
        }

        m_waveCoroutine = StartCoroutine(SpawnWave(m_waves.Waves[index]));
        return true;
    }

    public void EndWave()
    {
        if(WaveRunning == false)
        {
            return;
        }

        ToSpawn.Value.Clear();
        ToSpawn.Refresh();

        IEnumerable<Health> enemies = new List<Health>(EnemiesRemaining.Value);

        foreach(Health enemy in enemies)
        {
            enemy.Kill();
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        CurrentWave.Value = wave;

        float time = 0f;
        ToSpawn.Value = ProcessEnemyInfos(wave.Enemies);
        List<EnemySpawnInfo> spawnedThisFrame = new List<EnemySpawnInfo>();

        while (ToSpawn.Value.Count > 0)
        {
            spawnedThisFrame.Clear();

            foreach (EnemySpawnInfo info in ToSpawn.Value)
            {
                if(info.Time < time)
                {
                    spawnedThisFrame.Add(info);

                    Health health = Spawn(info);

                    EnemiesRemaining.Value.Add(health);
                    EnemiesRemaining.Refresh();

                    health.OnDeath += () =>
                    {
                        OnDeath(health);
                    };
                }
            }

            //Remove elements from list after loop is complete
            spawnedThisFrame.ForEach(info => ToSpawn.Value.Remove(info));
            ToSpawn.Refresh();

            yield return null;

            time += Time.deltaTime;
        }

        m_waveCoroutine = null;
    }

    private void OnDeath(Health health)
    {
        EnemiesRemaining.Value.Remove(health);
        EnemiesRemaining.Refresh();

        Debug.Log($"Spawned enemy dies: there are {ToSpawn.Value.Count} left to spawn and {EnemiesRemaining.Value.Count} left to kill");

        if(ToSpawn.Value.Count <= 0 && EnemiesRemaining.Value.Count <= 0)
        {
            Debug.Log($"Completed wave {m_currentWave}");
            CurrentWave.Value = null;
        }
    }

    //Expands repeat info into multiple enemy spawn info entries
    private List<EnemySpawnInfo> ProcessEnemyInfos(IEnumerable<EnemySpawnInfo> enemyInfos)
    {
        List<EnemySpawnInfo> expandedList = new List<EnemySpawnInfo>();

        foreach (EnemySpawnInfo info in enemyInfos)
        {
            for (int i = 0; i < info.NumberToSpawn; i++)
            {
                //Set position to pre-defined spawn point if one is specified
                Vector3 pos = info.Position;
                SpawnPoint spawnPoint = m_spawnPoints.FirstOrDefault(sp => sp.Name == info.SpawnPointName);

                if(string.IsNullOrEmpty(info.SpawnPointName) == false && spawnPoint != null)
                {
                    pos = spawnPoint.Position;
                }

                expandedList.Add(new EnemySpawnInfo()
                {
                    ID = info.ID,
                    Position = pos,
                    Time = info.Time + info.RepeatTime * i
                });
            }
        }

        return expandedList;
    }

    protected Health Spawn(EnemySpawnInfo info)
    {
        MMObjectPooler pool = m_objectPools.FirstOrDefault(p => p.ID == info.ID).Pool;

        if(pool == null)
        {
            Debug.LogError($"Cannot find object pool for enemy ID {info.ID}");
            return null;
        }

        GameObject obj = pool.GetPooledGameObject();

        if (obj == null) 
        {
            Debug.LogError($"Cannot spawn object from pool for enemy ID {info.ID}");
            return null; 
        }

        //EW 14-07-23 Copied from TimedSpawner, but we needed more control over the spawning
        if (obj.GetComponent<MMPoolableObject>() == null)
        {
            throw new Exception(gameObject.name + " is trying to spawn objects that don't have a PoolableObject component.");
        }

        Health objectHealth = obj.MMGetComponentNoAlloc<Health>();

        if(objectHealth == null)
        {
            throw new Exception(gameObject.name + " requires the Health component but none could be found");
        }

        obj.transform.position = info.Position;
        obj.SetActive(true);
        obj.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();
        objectHealth.Revive();

        return objectHealth;
    }
}
