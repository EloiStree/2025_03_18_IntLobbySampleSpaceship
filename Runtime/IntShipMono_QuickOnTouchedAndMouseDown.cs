using System;
using UnityEngine;
using UnityEngine.Events;

public class IntShipMono_QuickOnTouchedAndMouseDown : MonoBehaviour
{

    public UnityEvent m_onTouched;
    public GameObject[] m_toDisableOnTouched;
    public bool m_destroyOnTouched = false;

    public void OnCollisionEnter(Collision collision)
    {
        
        Touched();
    }
    public void OnTriggerEnter(Collider other)
    {

        Touched();
    }

    public void OnMouseDown()
    {
        Touched();
    }

    [ContextMenu("Touch it")]
    public void Touched()
    {

        m_onTouched.Invoke();
        foreach (var item in m_toDisableOnTouched)
        {
            if (item != null)
                item.SetActive(false);
        }
        if (m_destroyOnTouched)
        {
            Destroy(gameObject);
        }
    }
}
