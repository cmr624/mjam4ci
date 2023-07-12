using Towers;
using UnityEngine;

public class TowerSandbox : MonoBehaviour
{
    [SerializeField]
    private TowerVisuals m_towerVisualsPrefab;
    [SerializeField]
    private Wallet m_wallet;

    void Start()
    {
        ITowerPlacementRule[] rules = new ITowerPlacementRule[]
        {
            new TPRMinimumDistance(1f),
        };

        TowerGroup greenGroup = new TowerGroup(name: "Green Group")
        {
            PlacementRules = rules,
            Calculator = new PLCMaxTowers(5),
        };

        greenGroup.PowerLevel.Subscribe(pl => Debug.Log($"Green group power level changed to {pl}"));

        m_wallet.Amount.Value = 10;

        Tower towerA = new Tower(new TowerStats()
        {
            MaxHealth = 11
        });
        Instantiate(m_towerVisualsPrefab).SetUp(towerA);

        greenGroup.AddTower(towerA);

        Tower towerB = new Tower(new TowerStats()
        {
            MaxHealth = 12
        });

        Result resultB = TowerGroupWalletWrapper.TryPlaceTower(greenGroup, m_wallet, towerB);

        if (resultB.Success)
        {
            Instantiate(m_towerVisualsPrefab).SetUp(towerB);
        }

        Tower towerC = new Tower(new TowerStats()
        {
            MaxHealth = 13
        });

        Result resultC = TowerGroupWalletWrapper.TryPlaceTower(greenGroup, m_wallet, towerC);

        if (resultC.Success)
        {
            Instantiate(m_towerVisualsPrefab).SetUp(towerC);
        }

    }
}
