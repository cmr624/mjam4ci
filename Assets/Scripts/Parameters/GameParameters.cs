using System.IO;
using UnityEngine;

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

    public Topic<Parameters> Parameters { get; } = new Topic<Parameters>(new Parameters());

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
    }

    public void Load()
    {
        string fileContents = File.ReadAllText($"{Application.persistentDataPath}/{m_filePath}");

        Parameters.Value = JsonUtility.FromJson<Parameters>(fileContents);
    }
}
