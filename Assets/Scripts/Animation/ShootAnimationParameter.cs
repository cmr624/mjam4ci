using MoreMountains.TopDownEngine;
using UnityEngine;

public class ShootAnimationParameter : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private CharacterHandleWeapon m_ability;
    [SerializeField]
    private string m_parameterName = "Shooting";

    private void Start()
    {
        m_ability.OnAbilityStart += OnShootingStarted;
        m_ability.OnAbilityStop += OnShootingStopped;
    }

    private void OnShootingStarted()
    {
        m_animator.SetBool(m_parameterName, true);
    }

    private void OnShootingStopped()
    {
        m_animator.SetBool(m_parameterName, false);
    }
}
