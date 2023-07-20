using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class FTUE : MonoBehaviour
{
    [SerializeField] private GameObject m_ftueCanvas;

    private void Start()
    {
        if (m_ftueCanvas == null)
        {
            Debug.LogError("FTUECanvas GameObject is missing in the scene.");
            return;
        }

        m_ftueCanvas.SetActive(false); // Make sure canvas is initially deactivated.

        int playCount = PlayerPrefs.GetInt("PlayCount", 0);
        if (playCount < 3)
        {
            Time.timeScale = 0; // Pause the game.
            m_ftueCanvas.SetActive(true); // Enable the FTUECanvas.
        }

        PlayerPrefs.SetInt("PlayCount", playCount + 1); // Increment the play count.
    }

    private void Update()
    {
        if (m_ftueCanvas.activeSelf && Input.anyKeyDown) // If any key is pressed and FTUECanvas is active
        {
            m_ftueCanvas.SetActive(false); // Hide the UI object.
            Time.timeScale = 1; // Resume the game.
        }
    }
}
