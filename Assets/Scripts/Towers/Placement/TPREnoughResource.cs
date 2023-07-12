using System.Collections.Generic;
using System.Linq;

/*
namespace Towers
{
    public class TPREnoughResource : ITowerPlacementRule
    {
        private Wallet m_wallet;
        private List<PurchaseableItem<Tower>> m_towerShop;

        public TPREnoughResource(Wallet wallet, List<PurchaseableItem<Tower>> towerShop)
        {
            m_wallet = wallet;
            m_towerShop = towerShop;
        }

        public TowerPlacementRuleResult CanPlace(Tower tower, TowerGroup _)
        {
            PurchaseableItem<Tower> shopItem = m_towerShop.FirstOrDefault(item => item.Item.TypeID == tower.TypeID);

            if (shopItem == null)
            { 
                return new TowerPlacementRuleResult()
                {
                    Valid = false,
                    Reason = "Could not find item in shop"
                };
            }

            if (shopItem.Cost > m_wallet.Amount.Value)
            {
                return new TowerPlacementRuleResult()
                {
                    Valid = false,
                    Reason = $"Only have {m_wallet.Amount.Value} resources but requires {shopItem.Cost}"
                };
            }

            return new TowerPlacementRuleResult() { Valid = true };
        }
    }
}
*/