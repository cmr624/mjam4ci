using System;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectSwitcher : MonoBehaviour
{
    [Serializable]
    public struct Option
    {
        public KeyCode KeyCode;
        public string DisplayName;
        public GameObject GameObject;
    }

    [SerializeField]
    private Option[] m_gameObjects;

    [SerializeField] private Toggle m_greenToggle;
    [SerializeField] private Toggle m_redToggle;
    [SerializeField] private Toggle m_blueToggle;
    public Topic<Option> Current { get; } = new Topic<Option>();

    private void Start()
    {
        for(int i = 1; i < m_gameObjects.Length; i++)
        {
            m_gameObjects[i].GameObject.SetActive(false);
        }

        if(m_gameObjects.Length > 0)
        {
            SetCurrent(m_gameObjects[0]);
        }
    }

    private void Update()
    {
        foreach(Option obj in m_gameObjects)
        {
            if (Input.GetKeyDown(obj.KeyCode))
            {
                SetCurrent(obj);
            }
        }
    }

    private void SetCurrent(Option option)
    {
        if (Current.Value.GameObject != null)
        {
            Current.Value.GameObject.SetActive(false);
        }

        if (option.GameObject != null)
        {
            option.GameObject.SetActive(true);
        }

        switch (option.KeyCode)
        {
            // Alpha1 = green toggle, alpha 2 = red toggle, alpha 3 = blue toggle
            case KeyCode.Alpha1:
                m_greenToggle.isOn = true;
                break;
            case KeyCode.Alpha2:
                m_redToggle.isOn = true;
                break;
            case KeyCode.Alpha3:
                m_blueToggle.isOn = true;
                break;
        }

        Current.Value = option;
    }
}
