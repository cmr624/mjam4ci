using System.Collections.Generic;
using System.Linq;

namespace Towers
{
    public class TowerGroup
    {
        public string Name { get; }
        public Topic<List<Tower>> Towers = new Topic<List<Tower>>(new List<Tower>());
        public IEnumerable<ITowerPlacementRule> PlacementRules { get; set; } = new List<ITowerPlacementRule>();

        public TowerGroup(string name)
        {
            Name = name;
        }

        public Result TryAddTower(Tower tower)
        {
            Result result = CanAddTower(tower);

            if (result.Success)
            {
                AddTower(tower);
            }

            return result;
        }

        public Result CanAddTower(Tower tower)
        {
            if (PlacementRules == null)
            {
                return new Result()
                {
                    Success = true
                };
            }

            List<Result> results = PlacementRules.Select(rule => rule.CanPlace(tower, this)).ToList();

            return Result.Condense(results);
        }

        /// <summary>
        /// Add a tower to the group ignoring placement rules
        /// </summary>
        /// <param name="tower"></param>
        public void AddTower(Tower tower)
        {
            Towers.Value.Add(tower);
            Towers.Refresh();
        }

        public void RemoveTower(Tower tower)
        {
            Towers.Value.Remove(tower);
            Towers.Refresh();
        }
    }
}