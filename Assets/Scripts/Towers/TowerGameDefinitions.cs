using Towers;
using UnityEngine;

public class TowerGameDefinitions : MonoBehaviour
{
    public enum TowerGroupColour
    {
        Red,
        Green,
        Blue
    }

    public TowerGroup RedGroup { get; private set; }
    public TowerGroup GreenGroup { get; private set; }
    public TowerGroup BlueGroup { get; private set; }

    private void Awake()
    {
        GreenGroup = new TowerGroup(name: "Green Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                new TPRMinimumDistance(minDistance: 5f),
                //Add placement rules here
            },
            //Define the calculation for power level here
            Calculator = new PLCMaxTowers(5),
        };

        RedGroup = new TowerGroup(name: "Red Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                new TPRMinimumDistance(minDistance: 10f),
                //Add placement rules here
            },
            //Define the calculation for power level here
            Calculator = new PLCMaxTowers(10),
        };

        BlueGroup = new TowerGroup(name: "Blue Group")
        {
            PlacementRules = new ITowerPlacementRule[]
            {
                new TPRMinimumDistance(minDistance: 20f),
                //Add placement rules here
            },
            //Define the calculation for power level here
            Calculator = new PLCMaxTowers(1),
        };

        //Add starting towers here:
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
