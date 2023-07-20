using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_currentWaveText;
    [SerializeField]
    private TextMeshProUGUI m_waveProgressText;
    [SerializeField]
    private WaveManager m_waveManager;

    [SerializeField] private GameObject m_promptText;

    private void Start()
    {
        m_waveManager.CurrentWave.Subscribe(SetCurrentWaveText);
        m_waveManager.CurrentWave.Subscribe(SetWaveProgressText);
        m_waveManager.ToSpawn.Subscribe(SetWaveProgressText);
        m_waveManager.EnemiesRemaining.Subscribe(SetWaveProgressText);
    }

    private void SetCurrentWaveText(Wave wave)
    {
        m_currentWaveText.text = wave != null ? $"Current Wave: {wave.Name}" : string.Empty;
    }

    private void SetWaveProgressText()
    {
        Wave wave = m_waveManager.CurrentWave.Value;

        if(wave == null)
        {
            m_waveProgressText.text = "Press ENTER to Start Wave";
            m_promptText.SetActive(true);
            return;
        }
        m_promptText.SetActive(false);
        string displayString = $"Enemies to spawn: {m_waveManager.ToSpawn.Value.Count}. Enemies To Destroy: {m_waveManager.EnemiesRemaining.Value.Count}";
        m_waveProgressText.text = displayString;
    }
}
