using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using TopDownEngineExtensions;
using UnityEngine;

public class WeaponModifier : MonoBehaviour
{
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;
    [SerializeField]
    private GameParametersLoader m_parameters;

    [SerializeField]
    private DamageOnTouch m_rifleProjectile;
    //TODO if we want them
    //[SerializeField]
    //private DamageOnTouch m_grenade;
    //[SerializeField]
    //private CharacterMultipleHandleWeapon m_melee;

    void Start()
    {
       // based on the current power level of the red tower, update all weapons' damage
       m_towerGameDefinitions.RedPowerLevel.Subscribe(UpdateWeapons);
    }

    private void UpdateWeapons(float powerLevel)
    {
        GameParameters parameters = m_parameters.Parameters;
        m_rifleProjectile.MinDamageCaused = Mathf.Lerp(parameters.RifleDamageMin, parameters.RifleDamageMax, powerLevel);
        m_rifleProjectile.MaxDamageCaused = Mathf.Lerp(parameters.RifleDamageMin, parameters.RifleDamageMax, powerLevel);

        Debug.Log($"Red Power Level set to {powerLevel}. Setting min rifle damage to {m_rifleProjectile.MinDamageCaused} and max to {m_rifleProjectile.MaxDamageCaused }");
    }
}
