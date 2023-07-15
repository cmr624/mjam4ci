using System;
using System.Linq;
using Towers;
using UnityEngine;

public class TowerGameDefinitions : MonoBehaviour
{
    public enum TowerType
    {
        Red,
        Green,
        Blue
    }

    [Serializable]
    private struct VisualsForType
    {
        public TowerType Type;
        public TowerVisuals Visuals;
    }

    [SerializeField]
    private VisualsForType[] m_visuals;

    public enum TowerGroupColour
    {
        Red,
        Green,
        Blue
        //Add new tower groups here then add an instance variable to this class and append to switch statement
    }

    public TowerGroup RedGroup { get; private set; }
    public ClampedFloatTopic RedPowerLevel { get; } = new ClampedFloatTopic(min: 0f, max: 1f);

    public TowerGroup GreenGroup { get; private set; }
    public TowerGroup BlueGroup { get; private set; }
    public ClampedFloatTopic BluePowerLevel { get; } = new ClampedFloatTopic(min: 0f, max: 1f);

    private GameParameters m_parameters;
    private TeamGameDefinitions m_teams;

    public void Create(GameParameters parameters, TeamGameDefinitions teamGameDefinitions)
    {
        m_parameters = parameters;
        m_teams = teamGameDefinitions;

        GreenGroup = new TowerGroup(name: "Green Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                new TPRMinimumDistance(minDistance: parameters.GreenTowerMinDistance) //, <- this rule only considers towers in its own group. See below for rule that considers all towers
                //Add placement rules here
            },
        };

        RedGroup = new TowerGroup(name: "Red Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                //new TPRMinimumDistance(minDistance: 10f),
                //Add placement rules here
            },
        };

        IPowerLevelCalculator redGroupPowerLevelCalculator = new PLCMaxTowers(10);
        RedGroup.Towers.Subscribe(towers =>
        {
            RedPowerLevel.Value = redGroupPowerLevelCalculator.CalculatePowerLevel(towers);
        });

        BlueGroup = new TowerGroup(name: "Blue Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                new TPRTowerLimit(parameters.BlueGroupMaxTowers),
                //Add placement rules here
            },
        };

        IPowerLevelCalculator blueGroupPowerLevelCalculator = new PLCMaxTowers(parameters.BlueGroupMaxTowers);
        BlueGroup.Towers.Subscribe(towers =>
        {
            BluePowerLevel.Value = blueGroupPowerLevelCalculator.CalculatePowerLevel(towers);
        });

        TowerGroup[] groups = new TowerGroup[]
        {
            RedGroup,
            GreenGroup,
            BlueGroup,
        };

        //Rule to enforce towers are all min distance apart from eachother.
        ITowerPlacementRule minDistRule = new TPRMinimumDistanceFromGroups(minDistance: parameters.AnyTowerMinDistance, groups);

        RedGroup.PlacementRules = RedGroup.PlacementRules.Append(minDistRule);
        GreenGroup.PlacementRules = GreenGroup.PlacementRules.Append(minDistRule);
        BlueGroup.PlacementRules = BlueGroup.PlacementRules.Append(minDistRule);

        //Add starting towers here if necessary
    }

    public TowerGroup GetGroup(TowerGroupColour colour)
    {
        return colour switch
        {
            TowerGroupColour.Red => RedGroup,
            TowerGroupColour.Green => GreenGroup,
            TowerGroupColour.Blue => BlueGroup,
            _ => null,
        };
    }

    //EW 15-07-23
    //Probably want to split these functions into nice factories but that's a tidiness thing
    public Tower CreateRedTower()
    {
        Tower redTower = new Tower()
        {
            Cost = m_parameters.RedTower.Cost,
            MaxHealth = m_parameters.RedTower.MaxHealth,
        };

        //Add custom functionality here

        redTower.OnDestroyed += () =>
        {
            RedGroup.RemoveTower(redTower);
        };

        return redTower;
    }

    public Tower CreateGreenTower()
    {
        Tower greenTower = new Tower()
        {
            Cost = m_parameters.GreenTower.Cost,
            MaxHealth = m_parameters.GreenTower.MaxHealth,
        };

        //Add custom functionality here

        greenTower.OnDestroyed += () =>
        {
            GreenGroup.RemoveTower(greenTower);
        };

        return greenTower;
    }

    public Tower CreateBlueTower()
    {
        Tower blueTower = new Tower()
        {
            Cost = m_parameters.BlueTower.Cost,
            MaxHealth = m_parameters.BlueTower.MaxHealth,
        };

        //Add custom functionality here

        blueTower.OnDestroyed += () =>
        {
            BlueGroup.RemoveTower(blueTower);
        };

        return blueTower;
    }

    public Tower CreateTower(TowerType type)
    {
        return type switch
        {
            TowerType.Red => CreateRedTower(),
            TowerType.Green => CreateGreenTower(),
            TowerType.Blue => CreateBlueTower(),
            _ => null,
        };
    }

    public TowerVisuals CreateRedTowerVisuals(Tower tower)
    {
        TowerVisuals visuals = InstantiateTowerVisuals(TowerType.Red);
        visuals.SetUp(tower);

        //Add custom functionality here

        return visuals;
    }

    public TowerVisuals CreateGreenTowerVisuals(Tower tower)
    {
        TowerVisuals visuals = InstantiateTowerVisuals(TowerType.Green);
        visuals.SetUp(tower);

        //Add custom functionality here

        TBModifyHealthInRange behaviour = visuals.gameObject.AddComponent<TBModifyHealthInRange>();
        behaviour.Tower = tower;
        behaviour.Range = m_parameters.GreenTowerHealRadius;
        behaviour.TimeBetweenHealing = m_parameters.GreenTowerTimeBetweenHealing;
        behaviour.Amount = m_parameters.GreenTowerHealAmount;
        behaviour.Teams = new Team[]
        {
            m_teams.PlayerTeam
        };

        return visuals;
    }

    public TowerVisuals CreateBlueTowerVisuals(Tower tower)
    {
        TowerVisuals visuals = InstantiateTowerVisuals(TowerType.Blue);
        visuals.SetUp(tower);

        //Add custom functionality here

        return visuals;
    }

    public TowerVisuals CreateTowerVisuals(TowerType type, Tower tower)
    {
        return type switch
        {
            TowerType.Red => CreateRedTowerVisuals(tower),
            TowerType.Green => CreateGreenTowerVisuals(tower),
            TowerType.Blue => CreateBlueTowerVisuals(tower),
            _ => null,
        };
    }

    private TowerVisuals InstantiateTowerVisuals(TowerType type)
    {
        TowerVisuals prefab = m_visuals.FirstOrDefault(v => v.Type == type).Visuals;

        if (prefab == null)
        {
            Debug.LogError($"Could not find tower visuals prefab for {type}");
            return null;
        }

        return Instantiate(prefab);
    }
}
