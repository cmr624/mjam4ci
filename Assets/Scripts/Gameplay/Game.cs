using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameParametersLoader m_parameters;
    [SerializeField]
    private Wallet m_wallet;
    [SerializeField]
    private TowerGameDefinitions m_towerGameDefinitions;
    [SerializeField]
    private TeamGameDefinitions m_teamGameDefinitions;

    void Awake()
    {
        m_parameters.Load();

        m_wallet.Amount.Value = m_parameters.Parameters.StartingResource;

        m_teamGameDefinitions.Create();

        m_towerGameDefinitions.Create(m_parameters.Parameters, m_teamGameDefinitions);
    }
}
