using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class AnimationBoolParameter : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private string m_parameterName;

    public void OnEnable()
    {
        m_animator.SetBool(m_parameterName, true);
        Debug.Log("AnimationBoolParameter: Setting to true");
    }

    public void OnDisable()
    {
        m_animator.SetBool(m_parameterName, false);
        Debug.Log("AnimationBoolParameter: Setting to false");
    }
}
