using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class SelfDestructAnimationParameter : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private string m_parameterName = "Exploding";

    public void OnEnable()
    {
        m_animator.SetBool(m_parameterName, true);
    }
}
