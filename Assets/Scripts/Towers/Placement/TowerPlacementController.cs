using Towers;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_placeKey;

    [SerializeField]
    private TowerGameDefinitions.TowerType m_towerType;
    [SerializeField]
    private TowerGameDefinitions.TowerGroupColour m_groupColour;
    [SerializeField]
    private TowerGameDefinitions m_towerDefinitions;
    [SerializeField]
    private Wallet m_wallet;
    [SerializeField]
    private Transform m_playerTransform;
    [SerializeField]
    private TimedText m_errorText;
    [SerializeField]
    private Transform m_towerParent;

    private void Update()
    {
        if (Input.GetKeyDown(m_placeKey))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        m_errorText.ClearText();

        //Init the tower object
        Tower tower = m_towerDefinitions.CreateTower(m_towerType);
        tower.Position.Value = m_playerTransform.position;

        //Get the group
        TowerGroup group = m_towerDefinitions.GetGroup(m_groupColour);

        //Try to add the tower to the group
        Result result = TowerGroupWalletWrapper.TryPlaceTower(group, m_wallet, tower);

        Debug.Log(result);

        if (result.Success)
        {
            //If the ading was successful, create visuals for the tower
            m_towerDefinitions.CreateTowerVisuals(m_towerType, tower);
        }
        else
        {
            m_errorText.SetText(result.BriefReason);
        }
    }

    /*
     * TODO: instead of shitting out towers where the mech is, we could place them with the mouse
     * if we have time to implement such a controller.
    private bool m_inPlaceMode;

    private void Update()
    {
        if (Input.GetKeyDown(m_enterPlacementMode))
        {
            TogglePlacementMode();
        }

        if (m_inPlaceMode)
        {

        }
    }

    public void TogglePlacementMode()
    {
        if (m_inPlaceMode)
        {
            ExitPlacementMode();
        }
        else
        {
            EnterPlacementMode();
        }
    }

    public void EnterPlacementMode()
    {
        if (m_inPlaceMode)
        {
            return;
        }
    }

    public void ExitPlacementMode()
    {
        if(m_inPlaceMode == false)
        {
            return;
        }
    }
    */
}
