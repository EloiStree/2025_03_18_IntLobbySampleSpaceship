using UnityEngine;

namespace Eloi.IntAction
{

    public class IntLobbyActionMono_SetToggleGameObjectArray : DefaultIntegerListenAndEmitterEventMono
    {
        [Header("Enable Iteration")]
        public IntActionId m_intToEnableIteration = new IntActionId(1801);
        public IntActionId m_intToDisableIteration = new IntActionId(2801);
        public GameObject[] m_gameObjectsToToggle;

        

        [ContextMenu("Enable Objects without notification")]
        public void EnableAllGameObjectsWithoutNotification()
        {
            for (int i = 0; i < GetCount(); i++)
            {
                SetElementActiveWithoutNotification(i, true);
            }

        }
        [ContextMenu("Disable Objects without notification")]
        public void DisableAllGameObjectsWithoutNotification()
        {
            for (int i = 0; i < GetCount(); i++)
            {
                SetElementActiveWithoutNotification(i, false);
            }
        }

        [ContextMenu("Enable Objects with notification")]
        public void EnableAllGameObjects()
        {
            for (int i = 0; i < GetCount(); i++)
            {
                SetElementActiveWithNotification(i, true);
            }
        }
        [ContextMenu("Disable Objects with notification")]
        public void DisableAllGameObjects()
        {
            for (int i = 0; i < GetCount(); i++)
            {
                SetElementActiveWithNotification(i, false);
            }
        }


        [ContextMenu("RandomlyEnableWithNotification All")]
        public void RandomlyEnableWithNotificationAll()
        {
            for (int i = 0; i < GetCount(); i++)
            {
                bool active = Random.Range(0, 2) == 1;
                SetElementActiveWithNotification(i, active);
            }
        }
        [ContextMenu("RandomlyEnableWithNotification One")]
        public void RandomlyEnableWithNotificationOne()
        {

            int index = Random.Range(0, GetCount());
            bool active = Random.Range(0, 2) == 1;
            SetElementActiveWithNotification(index, active);
        }






        public int GetCount()
        {
            return m_gameObjectsToToggle.Length;
        }

        public bool IsElementSelfActive(int relativeIndexStartAt0)
        {
            if (relativeIndexStartAt0 < 0)
            {
                return false;

            }
            if (relativeIndexStartAt0 < m_gameObjectsToToggle.Length)
            {
                if (m_gameObjectsToToggle[relativeIndexStartAt0] != null)
                    return m_gameObjectsToToggle[relativeIndexStartAt0].activeSelf;
            }
            return false;

        }
        public void SetElementActiveWithoutNotification(int relativeIndexStartAt0, bool active)
        {
            if (relativeIndexStartAt0 < 0)
            {
                return;
            }

            if (relativeIndexStartAt0 < m_gameObjectsToToggle.Length)
            {
                if (m_gameObjectsToToggle[relativeIndexStartAt0] != null)
                    m_gameObjectsToToggle[relativeIndexStartAt0].SetActive(active);
            }
        }
        public void SetElementActiveWithNotification(int relativeIndexStartAt0, bool active)
        {

            if (relativeIndexStartAt0 < 0)
            {
                return;
            }

            if (relativeIndexStartAt0 < m_gameObjectsToToggle.Length)
            {
                if (m_gameObjectsToToggle[relativeIndexStartAt0] != null)
                {
                    bool isCurrentlyActive = m_gameObjectsToToggle[relativeIndexStartAt0].activeSelf;

                    if (isCurrentlyActive != active)
                    {
                        m_gameObjectsToToggle[relativeIndexStartAt0].SetActive(active);
                        if (active)
                        {
                            SendInteger(m_intToEnableIteration.Value + relativeIndexStartAt0);
                        }
                        else
                        {
                            SendInteger(m_intToDisableIteration.Value + relativeIndexStartAt0);
                        }

                    }
                }
            }
        }


        public void HandleBroadcastedInteger(int integerValue)
        {
            int lenght = m_gameObjectsToToggle.Length;
            int enableStart = m_intToEnableIteration.Value;
            int disableStart = m_intToDisableIteration.Value;
            int enableStop = m_intToEnableIteration.Value + lenght;
            int disableStop = m_intToDisableIteration.Value + lenght;

       
            if (integerValue >= enableStart && integerValue < enableStop)
            {
                int index = integerValue - enableStart;
                if (index < lenght)
                {
                 
                    SetElementActiveWithNotification(index, true);
                }
            }
            if (integerValue >= disableStart && integerValue < disableStop)
            {
                int index = integerValue - disableStart;
                if (index < lenght)
                {
                    SetElementActiveWithNotification(index, false);
                }
            }
        }



        public bool IsOneActive()
        {
            for (int i = 0; i < m_gameObjectsToToggle.Length; i++)
            {
                if (m_gameObjectsToToggle[i] != null && m_gameObjectsToToggle[i].activeSelf)
                    return true;
            }
            return false;
        }
        public bool IsOneInactive()
        {
            for (int i = 0; i < m_gameObjectsToToggle.Length; i++)
            {
                if (m_gameObjectsToToggle[i] != null && !m_gameObjectsToToggle[i].activeSelf)
                    return true;
            }
            return false;
        }
        public bool IsAllActive()
        {
            for (int i = 0; i < m_gameObjectsToToggle.Length; i++)
            {
                if (m_gameObjectsToToggle[i] != null && !m_gameObjectsToToggle[i].activeSelf)
                    return false;
            }
            return true;
        }
        public bool IsAllInactive()
        {
            for (int i = 0; i < m_gameObjectsToToggle.Length; i++)
            {
                if (m_gameObjectsToToggle[i] != null && m_gameObjectsToToggle[i].activeSelf)
                    return false;
            }
            return true;
        }

    }


}



