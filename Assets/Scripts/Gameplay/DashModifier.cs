using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class DashModifier : MonoBehaviour
{
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;
    
    // get dash ability
    private CharacterDash3D m_dashAbility;
    // Start is called before the first frame update
    void Start()
    {
        m_dashAbility = GetComponent<CharacterDash3D>();
        // based on the blue power level in tower game definitions, update the dash ability speed 
        m_towerGameDefinitions.BluePowerLevel.Subscribe(UpdateDash);
    }

    // Update is called once per frame
    private void UpdateDash(float amount)
    {
       // modify speed and distance of dash ability by amount
       if (m_dashAbility.DashDuration > 0.1f)
       {
           m_dashAbility.DashDuration -= amount / 10;
       }

       if (m_dashAbility.DashDistance < 16)
       {
           m_dashAbility.DashDistance += amount * 3;
       }
    }
}
