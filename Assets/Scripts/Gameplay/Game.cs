using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameParameters m_parameters;
    [SerializeField]
    private Wallet m_wallet;

    void Start()
    {
        m_parameters.Load();

        m_wallet.Amount.Value = m_parameters.Parameters.Value.StartingResource;
    }
}
