using TMPro;
using UnityEngine;

public class GameObjectSwitcherDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private string m_displayString = "{0}";
    [SerializeField]
    private GameObjectSwitcher m_switcher;

    private void Start()
    {
        m_switcher.Current.Subscribe(SetText);
    }

    private void SetText(GameObjectSwitcher.Option option)
    {
        m_text.text = string.Format(m_displayString, option.DisplayName);
    }
}
