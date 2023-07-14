using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class OnDeathGiveResource : MonoBehaviour
{
    // Start is called before the first frame update
   
    public float AmountOfResource = 10;
    void Start()
    {
        // on death, give resource to Wallet component in scene
        GetComponent<Health>().OnDeath += () => FindObjectOfType<Wallet>().Amount.Value += AmountOfResource;
    }
}
