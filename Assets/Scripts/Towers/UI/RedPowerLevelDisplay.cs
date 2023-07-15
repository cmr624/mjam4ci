using TMPro;
using UnityEngine;

public class RedPowerLevelDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private string m_dipslayString = "{0}";
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;

    private void Start()
    {
        m_towerGameDefinitions.RedPowerLevel.Subscribe(UpdateText);
    }

    private void UpdateText(float amount)
    {
        m_text.text = string.Format(m_dipslayString, amount);
    }
}
