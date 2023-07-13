namespace Towers
{
    //TPR = TowerPlacement Rule
    //Only considers towers in the group into which the tower
    //is trying to be added
    public class TPRMinimumDistance : ITowerPlacementRule
    {
        private float m_minDistance;

        public TPRMinimumDistance(float minDistance)
        {
            m_minDistance = minDistance;
        }

        Result ITowerPlacementRule.CanPlace(Tower tower, TowerGroup group)
        {
            Result result = new Result()
            {
                Success = true,
            };

            foreach(Tower t in group.Towers.Value)
            {
                float dist = (t.Position.Value - tower.Position.Value).magnitude;

                if(dist < m_minDistance)
                {
                    result.Success = false;
                    result.BriefReason = $"Too close to another tower";
                    result.FullReason += $"Tower {t} and Tower {tower} are {dist}m apart. They need to be {m_minDistance}m at a minimum.\n";
                }
            }

            return result;
        }
    }
}