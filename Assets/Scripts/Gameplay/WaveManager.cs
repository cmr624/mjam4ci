using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnInfo
{
    public string ID;
    public float Time;
    public Vector3 Position;
    public float RepeatTime;
    public int RepeatNumber;
}

public class Wave
{
    public string Name;
    public string Description;
    //public float Length; //,_ for now, go until all enemimes destroyed
    public EnemySpawnInfo[] Enemies;
}

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

    [SerializeField]
    private PoolID[] m_objectPools;

    //TEMP
    [SerializeField]
    private KeyCode m_startNextWaveKey;
    [SerializeField]
    private KeyCode m_endCurrentWave;
    //end TEMP

    private int m_currentWave;
    private AllWaves m_waves;
    private List<GameObject> m_enemies = new List<GameObject>();
    private Coroutine m_waveCoroutine;

    private void Awake()
    {
        //TODO: load from file so it can be changed without rebuilding

        m_waves = new AllWaves()
        {
            Waves = new Wave[]
            {
                new Wave
                {
                    Name = "Test Wave 1",
                    Enemies = new EnemySpawnInfo[]
                    {
                        new EnemySpawnInfo
                        {
                            ID = "Test",
                            Position = new Vector3(10f, 0f, 10f),
                            Time = 0,
                        }
                    }
                },
                new Wave
                {
                    Name = "Test Wave 2",
                    Enemies = new EnemySpawnInfo[]
                    {
                        new EnemySpawnInfo
                        {
                            ID = "Test",
                            Position = new Vector3(10f, 0f, 10f),
                            Time = 0,
                            RepeatTime = 1,
                            RepeatNumber = 5
                        },  
                        new EnemySpawnInfo
                        {
                            ID = "Test",
                            Position = new Vector3(10f, 0f, 10f),
                            Time = 7,
                            RepeatTime = 0.5f,
                            RepeatNumber = 10,
                        },
                    },
                },
            }
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_startNextWaveKey))
        {
            StartNextWave();
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
        if(m_waveCoroutine != null)
        {
            Debug.LogError($"Cannot start wave while a wave is already running");
            return false;
        }

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
        if(m_waveCoroutine != null)
        {
            StopCoroutine(m_waveCoroutine);
            m_waveCoroutine = null;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        float time = 0f;
        List<EnemySpawnInfo> toSpawn = ProcessEnemyInfos(wave.Enemies);
        List<EnemySpawnInfo> spawnedThisFrame = new List<EnemySpawnInfo>();

        while (toSpawn.Count > 0)
        {
            spawnedThisFrame.Clear();

            foreach (EnemySpawnInfo info in toSpawn)
            {
                if(info.Time < time)
                {
                    spawnedThisFrame.Add(info);

                    //TODO: on all enemies destroyed, end wave
                    m_enemies.Add(Spawn(info));
                }
            }

            //Remove elements from list after loop is complete
            spawnedThisFrame.ForEach(info => toSpawn.Remove(info));

            yield return null;

            time += Time.deltaTime;
        }

        m_waveCoroutine = null;
    }

    //Expands repeat info into multiple enemy spawn info entries
    private List<EnemySpawnInfo> ProcessEnemyInfos(IEnumerable<EnemySpawnInfo> enemyInfos)
    {
        List<EnemySpawnInfo> expandedList = new List<EnemySpawnInfo>();

        foreach (EnemySpawnInfo info in enemyInfos)
        {
            expandedList.Add(info);

            for (int i = 0; i < info.RepeatNumber; i++)
            {
                expandedList.Add(new EnemySpawnInfo()
                {
                    ID = info.ID,
                    Position = info.Position,
                    Time = info.Time + info.RepeatTime * i
                });
            }
        }

        return expandedList;
    }

    protected GameObject Spawn(EnemySpawnInfo info)
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

        obj.SetActive(true);
        obj.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

        Health objectHealth = obj.MMGetComponentNoAlloc<Health>();
        if (objectHealth != null)
        {
            objectHealth.Revive();
        }

        obj.transform.position = info.Position;

        return obj;
    }
}
