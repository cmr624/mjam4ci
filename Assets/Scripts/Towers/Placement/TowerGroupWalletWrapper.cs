namespace Towers
{
    public static class TowerGroupWalletWrapper 
    {
        public static Result TryPlaceTower(TowerGroup group, Wallet wallet, Tower tower)
        {
            Result result = CanPlaceTower(group, wallet, tower);

            if (result.Success)
            {
                group.AddTower(tower);
                wallet.Amount.Value -= tower.Cost;
            }

            return result;
        }

        public static Result CanPlaceTower(TowerGroup group, Wallet wallet, Tower tower)
        {
            if (wallet.Amount.Value < tower.Cost)
            {
                return new Result()
                {
                    Success = false,
                    Reason = $"Cannot afford: tower costs {tower.Cost} and you have {wallet.Amount.Value}"
                };
            }

            return group.CanAddTower(tower);
        }
    }
}