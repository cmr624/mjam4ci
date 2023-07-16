using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

[RequireComponent(typeof(CharacterDash3D))]
public class DashModifier : MonoBehaviour
{
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;
    [SerializeField]
    private GameParametersLoader m_parameters;
    
    private CharacterDash3D m_dashAbility;

    void Start()
    {
        m_dashAbility = GetComponent<CharacterDash3D>();
        
        // based on the blue power level in tower game definitions, update the dash ability speed 
        m_towerGameDefinitions.BluePowerLevel.Subscribe(UpdateDash);
    }

    private void UpdateDash(float powerLevel)
    {
        GameParameters parameters = m_parameters.Parameters;
        //m_dashAbility.DashDuration = Mathf.Lerp(parameters.DashTimeMin, parameters.DashTimeMax, powerLevel);
        //m_dashAbility.DashDistance = Mathf.Lerp(parameters.DashDistanceMin, parameters.DashDistanceMax, powerLevel);
        m_dashAbility.Cooldown.RefillDuration = Mathf.Lerp(parameters.DashCoolDownMax, parameters.DashCoolDownMin, powerLevel);
        //Debug.Log($"Blue Power Level set to {powerLevel}. Setting dash duration to {m_dashAbility.DashDuration} and distance to {m_dashAbility.DashDistance}");
        Debug.Log(
            $"Blue Power Level set to {powerLevel}. Setting dash refill duration to {m_dashAbility.Cooldown.RefillDuration}");
    }
}
