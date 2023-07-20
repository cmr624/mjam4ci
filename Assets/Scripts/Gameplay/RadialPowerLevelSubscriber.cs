using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class RadialPowerLevelSubscriber : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TowerGameDefinitions m_towerGameDefinitions;
   
    // get each power level from towerGameDefitions and store in private variable
    private Topic<float> m_redPowerLevel => m_towerGameDefinitions.RedPowerLevel;
    private Topic<float> m_bluePowerLevel => m_towerGameDefinitions.BluePowerLevel;
    
    public bool m_isRed;
    
    MMRadialProgressBar m_radialProgressBar;
    void Start()
    {
       m_radialProgressBar = GetComponent<MMRadialProgressBar>();
       if (m_isRed)
       {
          m_redPowerLevel.Subscribe(UpdateRadial); 
       }
       else
       {
           m_bluePowerLevel.Subscribe(UpdateRadial);
       }
    }

    private void UpdateRadial(float amount)
    {
        m_radialProgressBar.UpdateBar(amount, 0, 1);
    }

}
