using System.Linq;
using Towers;
using UnityEngine;

/// <summary>
/// Testing class
/// </summary>
public class Testing_DestroyOldestTower : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_destroyKey;
    [SerializeField]
    private KeyCode m_damageKey;
    [SerializeField]
    private TowerGameDefinitions m_towerDefinitions;
    [SerializeField]
    private TowerGameDefinitions.TowerGroupColour m_group;

    private void Update()
    {
        if (Input.GetKeyDown(m_destroyKey))
        {
            DestroyOldest();
        }

        if (Input.GetKeyDown(m_damageKey))
        {
            DamageOldest();
        }
    }

    private void DestroyOldest()
    {
        m_towerDefinitions.Ledger.DestroyTower(GetOldest());
    }

    private void DamageOldest()
    {
        Tower tower = GetOldest();

        if(tower == null)
        {
            return;
        }

        tower.Health.Value -= 10f;
    }

    private Tower GetOldest()
    {
        TowerGroup group = m_towerDefinitions.GetGroup(m_group);

        if (group.Towers.Value.Count <= 0)
        {
            return null;
        }

        return group.Towers.Value.FirstOrDefault();
    }
}
