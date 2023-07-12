using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private float m_startingAmount;

    public Topic<float> Amount { get; } = new Topic<float>();

    private void Awake()
    {
        Amount.Value = m_startingAmount;

        Amount.Subscribe(amount => Debug.Log($"Wallet has {amount} resource"));
    }
}
