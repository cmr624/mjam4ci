using System;
using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    [Serializable]
    private struct KeyForObject
    {
        public KeyCode KeyCode;
        public GameObject GameObject;
    }

    [SerializeField]
    private KeyForObject[] m_gameObjects;

    private GameObject m_current;

    private void Start()
    {
        for(int i = 0; i < m_gameObjects.Length; i++)
        {
            //Set the first object enabled, disable the rest
            m_gameObjects[i].GameObject.SetActive(i == 0);
        }
    }

    private void Update()
    {
        foreach(KeyForObject obj in m_gameObjects)
        {
            if (Input.GetKeyDown(obj.KeyCode))
            {
                if(m_current != null)
                {
                    m_current.SetActive(false);
                }

                m_current = obj.GameObject;
                m_current.SetActive(true);
            }
        }
    }
}
