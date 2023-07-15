using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerLevelDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private string m_dipslayString = "{0}";
    [SerializeField]
    private TowerGameDefinitions.TowerGroupColour m_colour;
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;

    private void Start()
    {
        m_towerGameDefinitions.GetGroup(m_colour).PowerLevel.Subscribe(UpdateText);
    }

    private void UpdateText(float amount)
    {
        m_text.text = string.Format(m_dipslayString, amount);
    }
}