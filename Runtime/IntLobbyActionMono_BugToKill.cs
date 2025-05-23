﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Eloi.IntAction
{


    public class IntLobbyActionMono_BugToKill : DefaultIntegerListenAndEmitterEventMono
    {

        [Header("Kill")]
        public IntActionId m_inOutBugKilled = new IntActionId(431001);
        public bool m_isKilled = false;
        public UnityEvent m_onKilledRequested;

        [Header("Spawn")]
        public IntActionId m_inOutBugSpawned = new IntActionId(432001);
        public bool m_isSpawned = false;
        public UnityEvent m_onSpawnedRequested;

        public IntActionId m_inResetRequest = new IntActionId(420000);
        public UnityEvent m_onResetRequest;

        public bool m_findBugsToKillInChildrenAtAwake = false;
        public Transform m_whereAreBugsStore;

        private void Reset()
        {
            m_whereAreBugsStore = transform;
        }
        private void Awake()
        {
            if (m_findBugsToKillInChildrenAtAwake)
            {
                IntLobbyActionMono_BugToKill[] bugsToKill = m_whereAreBugsStore.GetComponentsInChildren<IntLobbyActionMono_BugToKill>();
                for (int i = 0; i < bugsToKill.Length; i++)
                {
                    if (bugsToKill[i] != this)
                    {
                        bugsToKill[i].NotifyAsSpawned();
                    }
                }
            }
        }

        [ContextMenu("Notify as killed")]
        public void NotifyAsKilled()
        {
            if (!m_isKilled)
            {
                m_isKilled = true;
                SendInteger(m_inOutBugKilled);
                m_onKilledRequested.Invoke();
            }
        }


        [ContextMenu("Notify as Spawn")]
        public void NotifyAsSpawned()
        {
            if (!m_isSpawned)
            {
                m_isSpawned = true;
                SendInteger(m_inOutBugSpawned);
                m_onSpawnedRequested.Invoke();
            }
        }


        public void HandleBroadcastedInteger(int integerValue)
        {
            if (m_inOutBugKilled == integerValue)
            {
                NotifyAsKilled();
            }
            else if (m_inOutBugSpawned == integerValue)
            {
                NotifyAsSpawned();
            }
            else if (m_inResetRequest == integerValue)
            {
                ResetToBeUsed();
            }
        }

        public bool IsSpawned()
        {
            return m_isSpawned;
        }

        public bool IsKilled()
        {
            return m_isKilled;
        }

        [ContextMenu("Reset to be used")]
        public void ResetToBeUsed()
        {
            m_isSpawned = false;
            m_isKilled = false;
            m_onResetRequest.Invoke();
        }
    }


}



