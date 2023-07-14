using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Topic<float> Amount { get; } = new Topic<float>();
}
