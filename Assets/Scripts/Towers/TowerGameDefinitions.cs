using System;
using System.Linq;
using Towers;
using UnityEngine;

public class TowerGameDefinitions : MonoBehaviour
{
    //There's probably a more extensible way to do this but it's the simplest way for the jam
    public enum TowerType
    {
        Red,
        Green,
        Blue,
        //e.g. BigRed
        //Add new tower types here and add in inspector
    }

    //EW 15-07-23: Move to game parameters for easier adjustment?
    [Serializable]
    public struct TowerDefintion 
    {
        public TowerType Type;
        public TowerStats Stats;
        public TowerVisuals Visuals;
    }

    [SerializeField]
    private TowerDefintion[] m_towerDefinitions;

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

    public TowerLedger Ledger { get; private set; }

    public void Create(GameParameters parameters)
    {
        Ledger = new TowerLedger();

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

    public TowerDefintion GetType(TowerType type)
    {
        return m_towerDefinitions.FirstOrDefault(def => def.Type == type);
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
}
