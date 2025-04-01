using UnityEngine;

public class ShipMono_RotateAroundAxis : MonoBehaviour
{
    private void Reset()
    {
        m_whatToRotate = transform;
    }
    public Transform m_whatToRotate;
    public Vector3 m_rotationAxis = Vector3.up;
    public Space m_rotationType = Space.Self;
    public float m_rotationMultiplicatorSpeed=360;
    public bool m_isRotating = true;
    public bool m_inverse = false;

    public void SetRotationSpeed(float speed)
    {
        m_rotationMultiplicatorSpeed = speed;
    }
    public void SetAsRotatingOnOff(bool isRotating)
    {
        m_isRotating = isRotating;
    }
    public void SetAsRotatingOn()
    {
        m_isRotating = true;
    }
    public void SetAsRotatingOff()
    {
        m_isRotating = false;
    }
    public void SetAsInverse(bool isInverse)
    {
        m_inverse = isInverse;
    }
    public void Update()
    {

        if (m_isRotating)
        {
            float rotationSpeed = m_rotationMultiplicatorSpeed * Time.deltaTime;
            if (m_inverse)
            {
                rotationSpeed = -rotationSpeed;
            }
            m_whatToRotate.Rotate(m_rotationAxis, rotationSpeed, m_rotationType);
        }

    }
}
