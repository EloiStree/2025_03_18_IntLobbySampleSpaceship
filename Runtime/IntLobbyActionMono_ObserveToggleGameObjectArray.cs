
using System.Collections.Generic;
using UnityEngine;

namespace Eloi.IntAction
{
    public class IntLobbyActionMono_ObserveToggleGameObjectArray : DefaultIntegerListenAndEmitterEventMono
    {
        [Header("Enable Iteration")]
        public IntActionId m_intToEnableIteration = new IntActionId(1801);
        public IntActionId m_intToDisableIteration = new IntActionId(2801);
        public GameObject[] m_gameObjectsToToggle;
        public List<ObserverGameObjects> m_observerGameObjects = new List<ObserverGameObjects>();

        [System.Serializable]
        public class ObserverGameObjects {

            public GameObject m_observedGameObject;
            public bool m_isActiveAtLastCheck;

            public void RefreshIsActiveFromObservedGameObject()
            {
                if (m_observedGameObject == null)
                {
                    m_isActiveAtLastCheck = false;
                    return;
                }
                m_isActiveAtLastCheck = m_observedGameObject.activeSelf;
            }

            public void SetAsActive(bool isActive,out bool changed)
            {
                if (m_observedGameObject == null)
                {
                    changed = false;
                    return;
                }
                changed = false;
                if (m_observedGameObject.activeSelf != isActive)
                {
                    m_observedGameObject.SetActive(isActive);
                    m_isActiveAtLastCheck = isActive;
                    changed = true;
                }
            }

            public void IsObjectChanged(out bool changed, out bool isActive)
            {
                changed = false;
                isActive = false;
                if (m_observedGameObject == null)
                {
                    return;
                }

                bool isCurrentlyActive= m_observedGameObject.activeSelf;
                isActive = isCurrentlyActive;
                if (isCurrentlyActive != m_isActiveAtLastCheck)
                {
                    changed = true;
                    m_isActiveAtLastCheck = isCurrentlyActive;
                }                
            }

            public GameObject GetGameObject()
            {
                return m_observedGameObject;
            }
        }

        public bool m_populateAtAwakeList = true;
        private void Awake()
        {
            if (m_populateAtAwakeList)
            {
                PopulateObservedList();
            }
        }

        [ContextMenu("Populate Observed List")]
        public void PopulateObservedList()
        {
            m_observerGameObjects.Clear();
            for (int i = 0; i < m_gameObjectsToToggle.Length; i++)
            {
                ObserverGameObjects observerGameObject = new ObserverGameObjects();
                observerGameObject.m_observedGameObject = m_gameObjectsToToggle[i];
                observerGameObject.m_isActiveAtLastCheck = false;
                m_observerGameObjects.Add(observerGameObject);
            }
        }

        [ContextMenu("Refresh Observed List")]
        public void ObserverGameObjectsAndNotifyChanged()
        {
            for (int i = 0; i < m_observerGameObjects.Count; i++)
            {
                m_observerGameObjects[i].IsObjectChanged(out bool changed, out bool isActive);
                if (changed)
                {
                    if (isActive)
                    {
                        SendInteger(m_intToEnableIteration.Value + i);
                    }
                    else
                    {
                        SendInteger(m_intToDisableIteration.Value + i);
                    }
                }       
            }
        }


        protected override void ChildrenHandlerForIntegerAction(int integerValue)
        {
            int length = m_gameObjectsToToggle.Length;
            int enableStart = m_intToEnableIteration.Value;
            int disableStart = m_intToDisableIteration.Value;
            int enableStop = enableStart + length;
            int disableStop = disableStart + length;

            if (integerValue >= enableStart && integerValue < enableStop)
            {
                int index = integerValue - enableStart;
                if (index >= 0 && index < length)
                {
                    m_observerGameObjects[index].GetGameObject()?.SetActive(true);
                }
            }
             if (integerValue >= disableStart && integerValue < disableStop)
            {
                int index = integerValue - disableStart;
                if (index >= 0 && index < length)
                {
                    m_observerGameObjects[index].GetGameObject()?.SetActive(false);
                }
            }
        }

        public bool m_useUpdateObserver = false;

        public void Update()
        {
            if (m_useUpdateObserver)
            {
               ObserverGameObjectsAndNotifyChanged();
            }
        }
    
    }


}



