using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class MechGUIManager : GUIManager
{
    public MMProgressBar DashBar;
    public MMProgressBar MissilesBar;
    public MMProgressBar MeleeBar;

    public override void UpdateDashBars(float currentFuel, float minFuel, float maxFuel, string playerID)
    {
        if (DashBar == null)
        {
            return;
        }
        DashBar.UpdateBar(currentFuel, minFuel, maxFuel);
    }

    public void UpdateMissilesBar(float currentCooldownTimeLeft, float minTime, float maxTime)
    {
        if (MissilesBar == null)
        {
            return;
        }
        MissilesBar.UpdateBar(currentCooldownTimeLeft, minTime, maxTime);
    }
    
    public void UpdateMeleeBar(float currentCooldownTimeLeft, float minTime, float maxTime)
    {
        if (MeleeBar == null)
        {
            return;
        }
        MeleeBar.UpdateBar(currentCooldownTimeLeft, minTime, maxTime);
    }
}
