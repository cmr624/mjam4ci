using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Parameters
{
    public float StartingResource;
}

public class GameParameters : MonoBehaviour
{
    [Tooltip("Relative to the persistent data path")]
    [SerializeField]
    private string m_filePath = "Parameters.json";
    [SerializeField]
    private KeyCode m_loadKey = KeyCode.F1;
    [SerializeField]
    private KeyCode m_saveKey = KeyCode.F2;
    [SerializeField]
    private Parameters m_fallbackParameters;

    public Topic<Parameters> Parameters { get; } = new Topic<Parameters>(new Parameters());

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
        string parameters = JsonUtility.ToJson(Parameters.Value);

        File.WriteAllText($"{Application.persistentDataPath}/{m_filePath}", parameters);

        Debug.Log($"Saved parameters to {Application.persistentDataPath}/{m_filePath}");
    }

    public void Load()
    {
        if (File.Exists(FilePath))
        {
            string fileContents = File.ReadAllText(FilePath);

            Parameters.Value = JsonUtility.FromJson<Parameters>(fileContents);

            Debug.Log($"Loaded parameters from {FilePath}");
        }
        else
        {
            Parameters.Value = m_fallbackParameters;

            Debug.Log($"Loaded parameters from fallback");
        }
    }
}
