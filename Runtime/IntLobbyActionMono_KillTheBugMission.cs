using UnityEngine;

namespace Eloi.IntAction
{
    public class IntLobbyActionMono_KillTheBugMission : DefaultIntegerListenAndEmitterEventMono
    {
            public IntActionId m_atLeastOneBugSpawned = new IntActionId(421001);
            public IntActionId m_atAllBugSpawned = new IntActionId(421002);
            public IntActionId m_allBugKilled = new IntActionId(421003);
            public IntActionId m_bugsMissionComplete = new IntActionId(421004);
            public IntActionId m_resetRequest = new IntActionId(421000);


            public bool m_isLeastOneBugSpawned = false;
            public bool m_isAllBugSpawned = false;
            public bool m_isBugKilled = false;
            public bool m_isMissionCompleted = false;

            public IntLobbyActionMono_BugToKill[] m_bugsToKill;

            public int m_bugSpawnRangeInt = 420000;
            public int m_bugKillRangeInt = 430000;


            [ContextMenu("Find bugs to kill in children")]
            public void FindBugsToKillInChildren()
            {

                m_bugsToKill = this.gameObject. GetComponentsInChildren<IntLobbyActionMono_BugToKill>(true);
            }
            [ContextMenu("Give Bugs id")]
            public void GiveBugKillSpawnId()
            {

                for (int i = 0; i < m_bugsToKill.Length; i++)
                {
                    m_bugsToKill[i].m_inOutBugKilled = new IntActionId( m_bugKillRangeInt + i);
                    m_bugsToKill[i].m_inOutBugSpawned = new IntActionId(m_bugSpawnRangeInt + i);
                }
            }

        [ContextMenu("Spawn Them All")]
        public void SpawnsThemAll()
        {
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    m_bugsToKill[i].NotifyAsSpawned();
                }
            }
        }
        [ContextMenu("Spawn Them All")]
        public void ResetThemAll()
        {
            for (int i = 0; i < m_bugsToKill.Length; i++)
            {
                if (m_bugsToKill[i] != null)
                {
                    m_bugsToKill[i].ResetToBeUsed();
                }
            }
        }
        [ContextMenu("Spawn Random")]
            public void SapwnRandomOne()
            {
                int randomIndex = Random.Range(0, m_bugsToKill.Length);
                if (m_bugsToKill[randomIndex] != null)
                {
                    m_bugsToKill[randomIndex].NotifyAsSpawned();
                }
            }
            [ContextMenu("Kill Them All")]
            public void KillAllBugs()
            {
                for (int i = 0; i < m_bugsToKill.Length; i++)
                {
                    if (m_bugsToKill[i] != null)
                    {
                        m_bugsToKill[i].NotifyAsKilled();
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
                        m_bugsToKill[i].IsSpawned()
                        ))
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
                        && !m_bugsToKill[i].IsSpawned())
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
                    if (m_bugsToKill[i] != null && !m_bugsToKill[i].IsKilled())
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
                    m_onIntegerActionEmitted.Invoke(m_atLeastOneBugSpawned.Value);
                    m_lastPushed = m_atLeastOneBugSpawned.Value;
                }
                if (allBugSpawned != m_isAllBugSpawned)
                {
                    m_isAllBugSpawned = allBugSpawned;
                    m_onIntegerActionEmitted.Invoke(m_atAllBugSpawned.Value);
                    m_lastPushed = m_atAllBugSpawned.Value;
                }
                if (allBugKilled != m_isBugKilled)
                {
                    m_isBugKilled = allBugKilled;
                    m_onIntegerActionEmitted.Invoke(m_allBugKilled.Value);
                    m_lastPushed = m_allBugKilled.Value;
                }

                bool isMissionComplete = allBugSpawned && allBugKilled;
                if (isMissionComplete != m_isMissionCompleted)
                {
                    m_isMissionCompleted = isMissionComplete;
                    if (m_isMissionCompleted)
                    {
                        m_onIntegerActionEmitted.Invoke(m_bugsMissionComplete.Value);
                        m_lastPushed = m_bugsMissionComplete.Value;
                    }
                }
            }

        protected override void ChildrenHandlerForIntegerAction(int integerValue)
        {
            if (integerValue == m_atAllBugSpawned)
            {
                SpawnsThemAll();
            }
            else if (m_allBugKilled == integerValue)
            {
                KillAllBugs();
            }
            else if (m_resetRequest == integerValue)
            {
                ResetThemAll();
            }
        }
    }


    }
    


