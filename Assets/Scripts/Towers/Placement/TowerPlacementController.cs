using Towers;
using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_placeKey;

    [SerializeField]
    private TowerStats m_stats;
    [SerializeField]
    private TowerVisuals m_towerVisualsPrefab;

    [SerializeField]
    private TowerGameDefinitions.TowerGroupColour m_groupColour;
    [SerializeField]
    private TowerGameDefinitions m_towerManager;
    [SerializeField]
    private Wallet m_wallet;
    [SerializeField]
    private Transform m_playerTransform;

    private void Update()
    {
        if (Input.GetKeyDown(m_placeKey))
        {
            Tower tower = new Tower(m_stats);
            tower.Position.Value = m_playerTransform.position;

            TowerGroup group = m_towerManager.GetGroup(m_groupColour);

            Result result = TowerGroupWalletWrapper.TryPlaceTower(group, m_wallet, tower);

            Debug.Log(result);

            if (result.Success)
            {
                TowerVisuals towerVisuals = Instantiate(m_towerVisualsPrefab, transform);
                towerVisuals.SetUp(tower);
            }
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
