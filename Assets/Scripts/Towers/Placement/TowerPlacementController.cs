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
    private TowerGameDefinitions m_towerManager;
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

        TowerGameDefinitions.TowerDefintion towerDef = m_towerManager.GetType(m_towerType);

        Tower tower = new Tower(towerDef.Stats);
        tower.Position.Value = m_playerTransform.position;

        TowerGroup group = m_towerManager.GetGroup(m_groupColour);

        Result result = TowerGroupWalletWrapper.TryPlaceTower(group, m_wallet, tower);

        Debug.Log(result);

        if (result.Success)
        {
            TowerVisuals towerVisuals = Instantiate(towerDef.Visuals, m_towerParent);
            towerVisuals.SetUp(tower);
        }
        else
        {
            m_errorText.SetText(result.BriefReason);
        }
    }

    /*
     * TODO: instead of shittong out towers where the mech is, we could place them with the mouse
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
