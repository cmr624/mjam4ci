using System;
using UnityEngine;

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

        Current.Value = option;
    }
}
