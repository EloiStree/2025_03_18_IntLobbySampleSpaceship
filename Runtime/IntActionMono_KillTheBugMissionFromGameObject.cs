using System;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.IntAction
{
    public class IntActionMono_KillTheBugMissionFromGameObject : MonoBehaviour, I_IntActionEmitter
    {

        public UnityEvent<int> m_onIntegerAction;
        public int m_lastPushed;
        public IntActionId m_atLeastBugSpawned = new IntActionId(420001);
        public IntActionId m_atAllBugSpawned = new IntActionId(420002);
        public IntActionId m_allBugKilled = new IntActionId(420003);
        public IntActionId m_bugsMissionComplete = new IntActionId(420004);

        public bool m_isLeastOneBugSpawned = false;
        public bool m_isAllBugSpawned = false;
        public bool m_isBugKilled = false;
        public bool m_isMissionCompleted = false;

        [ContextMenu("Hide Them All")]
        public void HideThemAll()
        {
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    m_bugsToKill[i].SetActive(false);
                }
            }
        }
        [ContextMenu("Spawn Them All")]
        public void SpawnsThemAll()
        {
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    m_bugsToKill[i].SetActive(true);
                }
            }
        }
        [ContextMenu("Spawn Random")]
        public void SapwnRandomOne()
        {
            int randomIndex = UnityEngine.Random.Range(0, m_bugsToKill.Length);
            if (m_bugsToKill[randomIndex] != null)
            {
                m_bugsToKill[randomIndex].SetActive(true);
            }
        }
        [ContextMenu("Kill Them All")]
        public void KillAllBugs()
        {
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    if (Application.isPlaying)
                        Destroy(m_bugsToKill[i]);
                }
            }
        }

        public bool IsOneBugSpawned()
        {
            bool isOneBugSpawned = false;
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] == null
                    || (m_bugsToKill[i] != null &&
                    m_bugsToKill[i].activeSelf)
                    )
                {
                    isOneBugSpawned = true;
                    break;
                }
            }
            return isOneBugSpawned;
        }
        public bool IsOneBugStillInactive()
        {
            bool isOneBugIsInNotActiveMode = false;
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null
                    && m_bugsToKill[i].activeSelf == false)
                {
                    isOneBugIsInNotActiveMode = true;
                    break;
                }
            }
            return isOneBugIsInNotActiveMode;
        }
        public bool IsAllBugKilled()
        {

            bool isAllBugsNullDestroyed = true;
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    isAllBugsNullDestroyed = false;
                    break;
                }
            }
            return isAllBugsNullDestroyed;
        }

        private void Update()
        {
            bool oneBugSpawned = false;
            bool allBugSpawned = true;
            bool allBugKilled = true;

            oneBugSpawned = IsOneBugSpawned();
            allBugSpawned = !IsOneBugStillInactive();
            allBugKilled = IsAllBugKilled();
            if (oneBugSpawned != m_isLeastOneBugSpawned)
            {
                m_isLeastOneBugSpawned = oneBugSpawned;
                m_onIntegerAction.Invoke(m_atLeastBugSpawned.Value);
                m_lastPushed = m_atLeastBugSpawned.Value;
            }
            if (allBugSpawned != m_isAllBugSpawned)
            {
                m_isAllBugSpawned = allBugSpawned;
                m_onIntegerAction.Invoke(m_atAllBugSpawned.Value);
                m_lastPushed = m_atAllBugSpawned.Value;
            }
            if (allBugKilled != m_isBugKilled)
            {
                m_isBugKilled = allBugKilled;
                m_onIntegerAction.Invoke(m_allBugKilled.Value);
                m_lastPushed = m_allBugKilled.Value;
            }

            bool isMissionComplete = allBugSpawned && allBugKilled;
            if (isMissionComplete != m_isMissionCompleted)
            {
                m_isMissionCompleted = isMissionComplete;
                if (m_isMissionCompleted)
                {
                    m_onIntegerAction.Invoke(m_bugsMissionComplete.Value);
                    m_lastPushed = m_bugsMissionComplete.Value;
                }
            }
        }
        public void AddEmissionListener(Action<int> listener)
        {
            m_onIntegerAction.AddListener(listener.Invoke);
        }

        public void RemoveEmissionListener(Action<int> listener)
        {
            m_onIntegerAction.RemoveListener(listener.Invoke);
        }

        public GameObject[] m_bugsToKill;

    }


    }
    


