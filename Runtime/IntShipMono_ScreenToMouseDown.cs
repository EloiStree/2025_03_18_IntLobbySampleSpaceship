using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// I am a dirty quickly improved classes to be able to use mouse down methode on gameobject
/// There is a better library in my github, this is just to make this package work without dependencies
/// </summary>
public class IntShipMono_ScreenToMouseDown : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference clickAction;   // Button (e.g., Mouse left click)
    public InputActionReference positionAction; // Vector2 (e.g., Pointer position)


    public Vector2 m_cursorPositionOnScreen;
    public bool m_isPressing;
    public Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (clickAction != null) { 
            clickAction.action.Enable();
            clickAction.action.performed += OnClickPerformed;
            clickAction.action.canceled += OnClickPerformed; 
        }
        if (positionAction != null)
        {
            positionAction.action.Enable();
            positionAction.action.performed += SetMousePosition;
            positionAction.action.canceled += SetMousePosition;
        }
    }

    private void SetMousePosition(InputAction.CallbackContext context)
    {
        m_cursorPositionOnScreen = positionAction.action.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        if (clickAction != null) { 
            
            clickAction.action.performed -= OnClickPerformed;
            clickAction.action.canceled -= OnClickPerformed;
        }
        if (positionAction != null)
        {
            positionAction.action.performed -= SetMousePosition;
            positionAction.action.canceled -= SetMousePosition;
        }
    }

    public LayerMask layerMask=~1;

    private int m_clickCount;
    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        bool value = context.ReadValue<float>() > 0.5f;
        if (value == m_isPressing)
            return;
        
        m_isPressing = value;
        if (mainCamera == null)
            mainCamera = Camera.main;
        if (mainCamera == null) return;

        if (positionAction == null) return;


        m_cursorPositionOnScreen = positionAction.action.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(m_cursorPositionOnScreen);

        GameObject[] hits = Physics.RaycastAll(ray, float.MaxValue, layerMask).Select(x => x.collider.gameObject).ToArray();

        Debug.Log("Click" + (m_clickCount++));
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (m_isPressing)
                {

                    Debug.Log("Click Down" + (m_clickCount++), hit);
                    hit.BroadcastMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
                }
                else {

                    Debug.Log("Click Up " + (m_clickCount++), hit);
                    hit.BroadcastMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        
    }
}
