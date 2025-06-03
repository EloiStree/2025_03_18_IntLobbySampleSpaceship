
using UnityEngine;
using UnityEngine.Events;

public class IntShipMono_CharPad : MonoBehaviour
{
    private static string alpha= "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private void Reset()
    {
        m_toPushOnTouch = alpha[Random.Range(0, alpha.Length)];
    }

    public char m_toPushOnTouch;
    public UnityEvent<char> m_onCharPushed;


    [ContextMenu("Push Char in Pad")]
    public void PushStoreCharInPad()
    {
        m_onCharPushed?.Invoke(m_toPushOnTouch);
    }
}
