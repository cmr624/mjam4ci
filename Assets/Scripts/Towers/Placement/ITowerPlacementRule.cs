
using System.Collections.Generic;
using System.Linq;

namespace Towers
{
    public struct Result
    {
        public bool Success;
        public string BriefReason;
        public string FullReason;

        public static Result Condense(List<Result> results)
        {
            if(results.Count() <= 0)
            {
                return new Result()
                {
                    Success = true
                };
            }

            return new Result()
            {
                Success = results.Select(result => result.Success).Aggregate((a, b) => a && b),
                BriefReason = results.Select(result => result.BriefReason).First(),
                FullReason = results.Select(result => result.FullReason).Aggregate((a, b) => $"{a}\n{b}"),
            };
        }

        public override string ToString()
        {
            return $"Success: {Success}\nReason: {FullReason}";
        }
    }

    public interface ITowerPlacementRule
    {
        public Result CanPlace(Tower tower, TowerGroup group);
    }
}