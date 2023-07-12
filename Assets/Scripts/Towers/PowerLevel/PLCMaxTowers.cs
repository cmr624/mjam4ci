using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    //PLC = Power Level Calculator
    public class PLCMaxTowers : IPowerLevelCalculator
    {
        private int m_maxTowers;

        public PLCMaxTowers(int maxTowers) 
        {
            m_maxTowers = Mathf.Max(1, maxTowers);
        }

        float IPowerLevelCalculator.CalculatePowerLevel(List<Tower> towers)
        {
            return Mathf.Clamp01(towers.Count / (float)m_maxTowers);
        }
    }
}
