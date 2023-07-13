using System.Collections;
using TMPro;
using UnityEngine;

public class TimedText : MonoBehaviour
{
    [SerializeField]
    private float m_defaultDisplayTime = 4f;
    [SerializeField]
    private TextMeshProUGUI m_text;

    private Coroutine m_coroutine;

    private void Start()
    {
        ClearText();
    }

    public void SetText(string text)
    {
        SetText(text, m_defaultDisplayTime);
    }

    public void ClearText()
    {
        SetText(string.Empty, 0);
    }

    public void SetText(string text, float time)
    {
        if(m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_text.text = text;

        if(string.IsNullOrEmpty(text) == false)
        {
            m_coroutine = StartCoroutine(ClearTextAfter(time));
        }
    }

    private IEnumerator ClearTextAfter(float time)
    {
        yield return new WaitForSeconds(time);

        m_text.text = string.Empty;
        m_coroutine = null;
    }
}
