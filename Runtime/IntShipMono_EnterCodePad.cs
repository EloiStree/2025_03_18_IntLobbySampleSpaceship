using System;
using UnityEngine;
using UnityEngine.Events;
public class IntShipMono_EnterCodePad: MonoBehaviour
{
    public string m_toEnterCode="123456";
    public string m_enterCode;
    public UnityEvent<string> m_onEnterCodeChanged;
    public UnityEvent m_onCodeValide;
    public UnityEvent m_onCodeFail;
    public UnityEvent<string> m_onCodeSubmitted;
    public UnityEvent m_onRequestToClearPad;



    public void EnqueueChar(char codeChar) {

        m_enterCode += codeChar;
        SubmitCodeEntered();
       
    }
    [ContextMenu("Clearn Code")]
    public void ClearCode()
    {
        m_enterCode = "";
        m_onRequestToClearPad.Invoke();
        m_onEnterCodeChanged.Invoke(m_enterCode);
    }

    public string m_isValideDebug;

    [ContextMenu("Submit Code Entered")]
    private void SubmitCodeEntered()
    {
        m_onEnterCodeChanged?.Invoke(m_enterCode);
        if (m_enterCode.Length < m_toEnterCode.Length)
        {
            return;
        }
        if (m_enterCode.IndexOf(m_toEnterCode)==0)
        {
            m_onCodeValide?.Invoke();
            m_isValideDebug =$"Code Valide {DateTime.Now.ToString()}";
        }
        else
        {
            m_onCodeFail?.Invoke();
            m_isValideDebug = $"Code Invalide {DateTime.Now.ToString()}";
        }
        m_onCodeSubmitted.Invoke(m_enterCode);
        ClearCode();

    }
}
