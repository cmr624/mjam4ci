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


    private CharacterMultipleHandleWeapon m_weapon;
    // Start is called before the first frame update
    void Start()
    {
       m_weapon = GetComponent<CharacterMultipleHandleWeapon>(); 
       // based on the current power level of the red tower, update all weapons' damage
       m_towerGameDefinitions.GetGroup(TowerGameDefinitions.TowerGroupColour.Red).PowerLevel.Subscribe(UpdateWeapons);
    }

    private void UpdateWeapons(float amount)
    {
         // get the current weapon's projectile data
         
          //weapon.CurrentWeapon += amount * 10;
         // m_weapon.CurrentWeapon.
          //    GetComponent<MMSimpleObjectPooler>().
           //   gameObject.GetComponent<Projectile>().
            //  GetComponent<DamageOnTouch>().MinDamageCaused
             // += amount * 10;
       
    }
}
