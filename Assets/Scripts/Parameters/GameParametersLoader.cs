     using System;
using System.IO;
using UnityEngine;

[Serializable]
public class TowerStats
{
    public float MaxHealth;
    public int Cost;
}

[Serializable]
public class GameParameters
{
    //Player
    public float StartingResource;

    //Towers
    public float AnyTowerMinDistance;

    [Header("Red Tower")]
    public TowerStats RedTower;
    public int RedGroupMaxTowers;
    public float RifleDamageMin;
    public float RifleDamageMax;
    [Header("Green Tower")]
    public TowerStats GreenTower;
    public float GreenTowerMinDistance;
    public float GreenTowerHealRadius;
    public float GreenTowerTimeBetweenHealing;
    public float GreenTowerHealAmount;
    [Header("Blue Tower")]
    public TowerStats BlueTower;
    public int BlueGroupMaxTowers;
    public float DashCoolDownMin;
    public float DashCoolDownMax;
    public float DashDistanceMin;
    public float DashDistanceMax;
    public float DashTimeMin;
    public float DashTimeMax;
}

public class GameParametersLoader : MonoBehaviour
{
    [Tooltip("Relative to the persistent data path")]
    [SerializeField]
    private string m_filePath = "Parameters.json";
    [SerializeField]
    private KeyCode m_loadKey = KeyCode.F1;
    [SerializeField]
    private KeyCode m_saveKey = KeyCode.F2;
    [SerializeField]
    private GameParameters m_fallbackParameters;

    public GameParameters Parameters { get; private set; } = new GameParameters();

    private string FilePath => $"{Application.persistentDataPath}/{m_filePath}";

    private void Update()
    {
        if (Input.GetKeyDown(m_loadKey))
        {
            Load();
        }

        if (Input.GetKeyDown(m_saveKey))
        {
            Save();
        }
    }

    public void Save()
    {
        string parameters = JsonUtility.ToJson(Parameters);

        File.WriteAllText($"{Application.persistentDataPath}/{m_filePath}", parameters);

        Debug.Log($"Saved parameters to {Application.persistentDataPath}/{m_filePath}");
    }

    public void Load()
    {
        if (File.Exists(FilePath))
        {
            string fileContents = File.ReadAllText(FilePath);

            Parameters = JsonUtility.FromJson<GameParameters>(fileContents);

            Debug.Log($"Loaded parameters from {FilePath}");
        }
        else
        {
            Parameters = m_fallbackParameters;

            Debug.Log($"Loaded parameters from fallback");
        }
    }
}
