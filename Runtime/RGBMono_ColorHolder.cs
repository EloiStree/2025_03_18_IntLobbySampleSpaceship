using UnityEngine;
using UnityEngine.Events;

public interface I_ColorIsSimilar
{
    bool IsSimilarColor(Color color);
}
public interface I_ColorHolder255RGB { 

    public void GetColor(out float red255, out float green255, out float blue255);

}

public class RGBMono_ColorHolder : MonoBehaviour, I_ColorHolder255RGB, I_ColorIsSimilar
{
    public Color color = Color.white;
    public UnityEvent<Color> m_onColorChanged = new UnityEvent<Color>();
    public float m_similarColorThreshold = 0.1f;

    public bool m_useAwakeRandomColor=true;

    void Awake()
    {
        if (m_useAwakeRandomColor)
            SetColor(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void SetColor(float r, float g, float b)
    {
        color = new Color(r, g, b);
        m_onColorChanged.Invoke(color);
    }
    public void SetColor(Color color)
    {
        this.color = color;
        m_onColorChanged.Invoke(color);
    }

    /// <summary>
    /// You should code a better one, it is a default one.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="percentThreshold"></param>
    /// <returns></returns>
    public bool IsSimilarColorDefaultCode(Color color, float percentThreshold=0.1f)
    {
        float r = Mathf.Abs(this.color.r - color.r);
        float g = Mathf.Abs(this.color.g - color.g);
        float b = Mathf.Abs(this.color.b - color.b);
        return r < percentThreshold && g < percentThreshold && b < percentThreshold;
    }

    public void GetColor(out float red255, out float green255, out float blue255)
    {
        red255 = Mathf.RoundToInt(color.r * 255f);
        green255 = Mathf.RoundToInt(color.g * 255f);
        blue255 = Mathf.RoundToInt(color.b * 255f);
    }
    public bool IsSimilarColor(Color color)
    {
        return IsSimilarColorDefaultCode(color, m_similarColorThreshold);
    }
}
