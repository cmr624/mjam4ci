using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private string m_dipslayString = "{0}";
    [SerializeField]
    private Wallet m_wallet;

    private void Start()
    {
        m_wallet.Amount.Subscribe(UpdateText);
    }

    private void UpdateText(float amount)
    {
        m_text.text = string.Format(m_dipslayString, amount);
    }
}
