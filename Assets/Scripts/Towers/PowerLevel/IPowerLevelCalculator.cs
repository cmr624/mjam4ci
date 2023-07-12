using System.Collections.Generic;

namespace Towers
{
    public interface IPowerLevelCalculator
    {
        public float CalculatePowerLevel(List<Tower> towers);
    }
}