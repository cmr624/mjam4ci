using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;
    
    private Health m_health;
    void Start()
    {
       m_health = GetComponent<Health>();
       // based on the current power level of the green tower in m_towerGameDefinitions update the health
       m_towerGameDefinitions.GetGroup(TowerGameDefinitions.TowerGroupColour.Green).PowerLevel.Subscribe(UpdateHealth);
    }

    // update health function
    private void UpdateHealth(float amount)
    {
        // TODO modify this math
        m_health.CurrentHealth += amount * 100;
        m_health.MaximumHealth += amount * 100;
        // update hte health bar
        m_health.UpdateHealthBar(true);
    }
}
