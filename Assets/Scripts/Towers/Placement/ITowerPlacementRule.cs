
using System.Collections.Generic;
using System.Linq;

namespace Towers
{
    public struct Result
    {
        public bool Success;
        public string Reason;

        public static Result Condense(List<Result> results)
        {
            return new Result()
            {
                Success = results.Select(result => result.Success).Aggregate((a, b) => a && b),
                Reason = results.Select(result => result.Reason).Aggregate((a, b) => $"{a}\n{b}"),
            };
        }

        public override string ToString()
        {
            return $"Success: {Success}\nReason: {Reason}";
        }
    }

    public interface ITowerPlacementRule
    {
        public Result CanPlace(Tower tower, TowerGroup group);
    }
}