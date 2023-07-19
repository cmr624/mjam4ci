using UnityEngine;

public class AnimationTriggerParameter : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private string m_parameterName;

    public void TriggerAnimation()
    {
        m_animator.SetTrigger(m_parameterName);
    }
}
