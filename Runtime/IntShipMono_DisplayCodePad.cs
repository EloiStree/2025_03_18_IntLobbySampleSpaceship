using UnityEngine;

public class IntShipMono_DisplayCodePad : MonoBehaviour {

    public string m_text;
    public Renderer[] m_digitRenderer;
    public Texture m_defaultTexture;

    [System.Serializable]
    public class CharToTexture
    {
        public char m_char;
        public Texture m_texture;
    }
    public CharToTexture[] m_charToTexture;


    private void Awake()
    {
        SetWithCurrentText();
    }

    [ContextMenu("Set Text with current")]
    public void SetWithCurrentText() {

        SetText(m_text);
    }
    public void SetText(string text)
    {
        m_text = text;
        if (!Application.isPlaying)
            return;
        for (int i = 0; i < m_digitRenderer.Length; i++)
        {
            if (i < m_text.Length)
            {
                char c = m_text[i];
                Texture t = GetTextureForChar(c);
                if (t != null)
                {
                    m_digitRenderer[i].material.mainTexture = t;
                }
                else
                {
                    m_digitRenderer[i].material.mainTexture = m_defaultTexture;
                }
            }
            else
            {
                m_digitRenderer[i].material.mainTexture = m_defaultTexture;
            }
        }
    }

    private Texture GetTextureForChar(char c)
    {
        foreach (var charToTexture in m_charToTexture)
        {
            if (charToTexture.m_char == c)
            {
                return charToTexture.m_texture;
            }
        }
        return null;
    }
}
