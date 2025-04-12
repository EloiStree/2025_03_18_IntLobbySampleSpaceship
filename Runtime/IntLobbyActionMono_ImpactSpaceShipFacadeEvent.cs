using Codice.Client.BaseCommands;
using Codice.Client.Common;
using System.Collections.Generic;
using Eloi.IntAction;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class IntLobbyActionMono_ImpactSpaceShipFacadeEvent : DefaultIntegerListenAndEmitterEventMono
{

    public bool m_useCoroutineToCheckMissions = true;
 

    public bool m_debugCheckIsMissionCompleted = false;
    public bool m_debugCheckIsAllBugsDeath = false;
    private IEnumerator CheckMissions()
    {
        while (true)
        {


            bool isAllBugsDeath =
                !m_toggleBlobBugCloud.GetCurrentValue()
                && !m_toggleBlobBugSlime.GetCurrentValue()
                && !m_toggleBugSpike.GetCurrentValue()
                && !m_toggleBlobBugFlamme.GetCurrentValue();

            bool isMissionBugCompleted = m_toggleMissionBlobKilled.GetCurrentValue();
            if (isAllBugsDeath && !isMissionBugCompleted)
            {
                m_toggleMissionBlobKilled.TurnOnAndPushInteger();
            }
            else if (!isAllBugsDeath && isMissionBugCompleted)
            {
                m_toggleMissionBlobKilled.TurnOffAndPushInteger();
            }


            bool isMissionCompleted = 
                m_toggleMissionEnterCode.GetCurrentValue() 
                && m_toggleMissionBlobKilled.GetCurrentValue();
            bool isMissionToggleCompleted = m_toggleMissionsCompleted.GetCurrentValue();
            if (isMissionCompleted && !isMissionToggleCompleted)
            {
                m_toggleMissionsCompleted.TurnOnAndPushInteger();
            }
            else if (!isMissionCompleted && isMissionToggleCompleted)
            {
                m_toggleMissionsCompleted.TurnOffAndPushInteger();
            }




            m_debugCheckIsAllBugsDeath = isAllBugsDeath;
            m_debugCheckIsMissionCompleted = isMissionCompleted;
            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override void ChildrenHandlerForIntegerAction(int integerValue)
    {
        base.ChildrenHandlerForIntegerAction(integerValue);

        m_reloadGameScene.HandleIntegerAction(integerValue);
        m_recalibrationScene.HandleIntegerAction(integerValue);
        m_toggleLight.HandleIntegerAction(integerValue);
        m_toggleShield.HandleIntegerAction(integerValue);
        m_toggleAlarm.HandleIntegerAction(integerValue);
        m_toggleMissionEnterCode.HandleIntegerAction(integerValue);
        m_toggleMissionBlobKilled.HandleIntegerAction(integerValue);
        m_toggleMissionsCompleted.HandleIntegerAction(integerValue);
        m_triggerGandalfSax.HandleIntegerAction(integerValue);
        m_resetGameStartTimeToZero.HandleIntegerAction(integerValue);
        m_setGameTimeInSeconds.HandleIntegerAction(integerValue);
        m_setLightColor.HandleIntegerAction(integerValue);
        m_toggleBlobBugCloud.HandleIntegerAction(integerValue);
        m_toggleBlobBugSlime.HandleIntegerAction(integerValue);
        m_toggleBugSpike.HandleIntegerAction(integerValue);
        m_toggleBlobBugFlamme.HandleIntegerAction(integerValue);
        m_gameOverMessage.HandleIntegerAction(integerValue);  
    }

    private void OnEnable()
    {
        if (m_useCoroutineToCheckMissions)
        {
            StartCoroutine(CheckMissions());
        }
        m_toggleAlarm.AddEmissionListener(SendInteger);
        m_toggleShield.AddEmissionListener(SendInteger);
        m_toggleLight.AddEmissionListener(SendInteger);
        m_toggleBlobBugCloud.AddEmissionListener(SendInteger);
        m_toggleBlobBugSlime.AddEmissionListener(SendInteger);
        m_toggleBugSpike.AddEmissionListener(SendInteger);
        m_toggleBlobBugFlamme.AddEmissionListener(SendInteger);
        m_toggleMissionEnterCode.AddEmissionListener(SendInteger);
        m_toggleMissionBlobKilled.AddEmissionListener(SendInteger);
        m_toggleMissionsCompleted.AddEmissionListener(SendInteger);
    }

    private void OnDisable()
    {
        m_toggleAlarm.RemoveEmissionListener(SendInteger);
        m_toggleShield.RemoveEmissionListener(SendInteger);
        m_toggleLight.RemoveEmissionListener(SendInteger);
        m_toggleBlobBugCloud.RemoveEmissionListener(SendInteger);
        m_toggleBlobBugSlime.RemoveEmissionListener(SendInteger);
        m_toggleBugSpike.RemoveEmissionListener(SendInteger);
        m_toggleBlobBugFlamme.RemoveEmissionListener(SendInteger);
        m_toggleMissionEnterCode.RemoveEmissionListener(SendInteger);
        m_toggleMissionBlobKilled.RemoveEmissionListener(SendInteger);
    }


    //Code In the Demo:
    //101 Load Game SceneIntLobbyAction_IntToUnityEventIntLobbyAction_IntToUnityEventIntLobbyAction_IntToUnityEventIntLobbyAction_IntToUnityEventIntLobbyAction_IntToUnityEventIntLobbyAction_IntToUnityEvent
    public IntLobbyAction_IntToUnityEvent m_reloadGameScene = new IntLobbyAction_IntToUnityEvent(101);

    [ContextMenu("Request to load Game Scene")]
    public void RequestToLoadGameScene()
    {
        m_reloadGameScene.InvokeWithCurrentInteger();
    }

    //401 Call Recalibration Scene
    public IntLobbyAction_IntToUnityEvent m_recalibrationScene = new IntLobbyAction_IntToUnityEvent(401);

    [ContextMenu("Request to load Game Scene")]
    public void RequestToLoadRecalibrationScene()
    {
        m_recalibrationScene.InvokeWithCurrentInteger();
    }


    //108 208 Light
    public IntLobbyAction_Toggle m_toggleLight = new IntLobbyAction_Toggle(108, 208);

    [ContextMenu("Turn on Light")]
    public void TurnOnLight() => m_toggleLight.TurnOnAndPushInteger();
    [ContextMenu("Turn off Light")]
    public void TurnOffLight() => m_toggleLight.TurnOffAndPushInteger();



    //102 202 Shield
    public IntLobbyAction_Toggle m_toggleShield = new IntLobbyAction_Toggle(102, 202);

    [ContextMenu("Turn on Shield")]
    public void TurnOnShield() => m_toggleShield.TurnOnAndPushInteger();
    [ContextMenu("Turn off Shield")]
    public void TurnOffShield() => m_toggleShield.TurnOffAndPushInteger();



    //103 203 Alarm
    public IntLobbyAction_Toggle m_toggleAlarm = new IntLobbyAction_Toggle(103, 203);


    [ContextMenu("Turn on Alarm")]
    public void TurnOnAlarm() => m_toggleAlarm.TurnOnAndPushInteger();

    [ContextMenu("Turn off Alarm")]
    public void TurnOffAlarm() => m_toggleAlarm.TurnOffAndPushInteger();



    //150 251 Code Enter Mission 
    public IntLobbyAction_Toggle m_toggleMissionsCompleted = new IntLobbyAction_Toggle(150, 250);

    //151 251 Code Enter Mission 
    public IntLobbyAction_Toggle m_toggleMissionEnterCode = new IntLobbyAction_Toggle(151, 251);
    //152 252 Bug Killed Mission
    public IntLobbyAction_Toggle m_toggleMissionBlobKilled = new IntLobbyAction_Toggle(152, 252);


    [ContextMenu("Missions Uncompleted")]
    public void ResetAllMissionsToUncompleted()
    {
        m_toggleMissionEnterCode.TurnOffAndPushInteger();
        m_toggleMissionBlobKilled.TurnOffAndPushInteger();
        m_toggleMissionsCompleted.TurnOffAndPushInteger();
    }

    [ContextMenu("Missions Completed")]
    public void SetMissionAsCompleted()
    {
        m_toggleMissionEnterCode.TurnOnAndPushInteger();
        m_toggleMissionBlobKilled.TurnOnAndPushInteger();
        m_toggleMissionsCompleted.TurnOnAndPushInteger();
    }

    [ContextMenu("Mission Completed:Code Entered")]
    public void SetCodeEnterMissionComplete()
    {
        m_toggleMissionEnterCode.TurnOnAndPushInteger();
    }
    [ContextMenu("Mission Completed:Blob Killed Entered")]
    public void SetBlobKilledMissionComplete()
    {
        m_toggleMissionBlobKilled.TurnOnAndPushInteger();
    }




    //7 Trigger Gandalf Sax for audio testing
    public IntLobbyAction_IntToUnityEvent m_triggerGandalfSax = new IntLobbyAction_IntToUnityEvent(7);

    [ContextMenu("Trigger Gandalf Sax")]
    public void TriggerGandalfSax() => m_triggerGandalfSax.InvokeWithCurrentInteger();


    //600 Reset current time start to zero
    public IntLobbyAction_IntToUnityEvent m_resetGameStartTimeToZero = new IntLobbyAction_IntToUnityEvent(600);
    [ContextMenu("Reset game timer to zero")]
    public void ResetGameTiming() => m_resetGameStartTimeToZero.InvokeWithCurrentInteger();


    public IntAction_IntegerToStringEvent m_gameOverMessage = new IntAction_IntegerToStringEvent(new IntToDataLink<string>[]
    {
        new IntToDataLink<string>(66100, "You Lost"),
        new IntToDataLink<string>(66101, "You Win"),
        new IntToDataLink<string>(66102, "Missing Air"),
        new IntToDataLink<string>(66103, "Meteor Collision"),
        new IntToDataLink<string>(66104, "One of the crew died"),
        new IntToDataLink<string>(66105, "Missing of power"),
        new IntToDataLink<string>(66106, "Missing of gaz"),
        new IntToDataLink<string>(66107, "Time out")
    });




    //601 Set Max Game Time to 1 minutes
    //602 Set Max Game Time to 2 minutes
    //603 Set Max Game Time to 3 minutes
    //604 Set Max Game Time to 4 minutes
    //605 Set Max Game Time to 5 minutes
    //606 Set Max Game Time to 6 minutes
    //607 Set Max Game Time to 7 minutes
    //608 Set Max Game Time to 8 minutes
    //609 Set Max Game Time to 9 minutes
    //610 Set Max Game Time to 10 minutes
    public IntAction_IntegerToFloatEvent m_setGameTimeInSeconds = new IntAction_IntegerToFloatEvent(new IntToDataLink<float>[] {

        new IntToDataLink<float>(601, 1f  *60f),
        new IntToDataLink<float>(602, 2f  *60f),
        new IntToDataLink<float>(603, 3f  *60f),
        new IntToDataLink<float>(604, 4f  *60f),
        new IntToDataLink<float>(605, 5f  *60f),
        new IntToDataLink<float>(606, 6f  *60f),
        new IntToDataLink<float>(607, 7f  *60f) ,
        new IntToDataLink<float>(608, 8f  *60f),
        new IntToDataLink<float>(609, 9f  *60f),
        new IntToDataLink<float>(610, 10f *60f),
    });

    public void SetGameTimeToOneMinute() => m_setGameTimeInSeconds.HandleIntegerAction(601);
    public void SetGameTimeToTwoMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(602);
    public void SetGameTimeToThreeMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(603);
    public void SetGameTimeToFourMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(604);
    public void SetGameTimeToFiveMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(605);
    public void SetGameTimeToSixMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(606);
    public void SetGameTimeToSevenMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(607);
    public void SetGameTimeToEightMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(608);
    public void SetGameTimeToNineMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(609);
    public void SetGameTimeToTenMinutes() => m_setGameTimeInSeconds.HandleIntegerAction(610);





    //700 Set Light to Red
    //701 Set Light to Green
    //702 Set Light to Blue
    //703 Set Light to Orange
    //704 Set Light to Yellow
    //705 Set Light to Purple
    //706 Set Light to Pink
    //707 Set Light to Cyan
    //708 Set Light to While
    //709 Set Light to Black

    public IntAction_IntegerToColorEvent m_setLightColor = new IntAction_IntegerToColorEvent(new IntToDataLink<Color>[] {
        new IntToDataLink<Color>(700, Color.red),
        new IntToDataLink<Color>(701, Color.green),
        new IntToDataLink<Color>(702, Color.blue),
        new IntToDataLink<Color>(703, new Color(1f, 0.5f, 0f)), // Orange
        new IntToDataLink<Color>(704, Color.yellow),
        new IntToDataLink<Color>(705, new Color(0.5f, 0f, 0.5f)), // Purple
        new IntToDataLink<Color>(706, Color.magenta),
        new IntToDataLink<Color>(707, Color.cyan),
        new IntToDataLink<Color>(708, Color.white),
        new IntToDataLink<Color>(709, Color.black),
    });


    [ContextMenu("Set light to red")]
    public void SetLightToRedDefault() => m_setLightColor.HandleIntegerAction(700);
    [ContextMenu("Set light to green")]
    public void SetLightToGreenDefault() => m_setLightColor.HandleIntegerAction(701);
    [ContextMenu("Set light to blue")]
    public void SetLightToBlueDefault() => m_setLightColor.HandleIntegerAction(702);

    [ContextMenu("Set light to white")]
    public void SetLightToWhiteDefault() => m_setLightColor.HandleIntegerAction(708);
    [ContextMenu("Set light to black")]
    public void SetLightToBlackDefault() => m_setLightColor.HandleIntegerAction(709);




 
    //801 901 Blob Bug Cloud is alive
    public IntLobbyAction_Toggle m_toggleBlobBugCloud = new IntLobbyAction_Toggle(801, 901);
    //802 902 Blob Bug Slime is alive
    public IntLobbyAction_Toggle m_toggleBlobBugSlime = new IntLobbyAction_Toggle(802, 902);
    //803 903 Blob Bug Spike is alive
    public IntLobbyAction_Toggle m_toggleBugSpike = new IntLobbyAction_Toggle(803, 903);
    //804 904 Blob Bug Flamme is alive
    public IntLobbyAction_Toggle m_toggleBlobBugFlamme = new IntLobbyAction_Toggle(804, 904);


    [ContextMenu("Turn blob on")]
    public void TurnAllBlobOn()
    {
        m_toggleBlobBugCloud.TurnOnAndPushInteger();
        m_toggleBlobBugSlime.TurnOnAndPushInteger();
        m_toggleBugSpike.TurnOnAndPushInteger();
        m_toggleBlobBugFlamme.TurnOnAndPushInteger();

    }

    [ContextMenu("Turn blob off")]
    public void TurnAllBlobOff()
    {
        m_toggleBlobBugCloud.TurnOffAndPushInteger();
        m_toggleBlobBugSlime.TurnOffAndPushInteger();
        m_toggleBugSpike.TurnOffAndPushInteger();
        m_toggleBlobBugFlamme.TurnOffAndPushInteger();
    }


    [ContextMenu("Turn blob off Cmpid")]
    public void TurnOffBlob_Cloud() =>m_toggleBlobBugCloud.TurnOffAndPushInteger();

    [ContextMenu("Turn blob off slime")] 
    public void TurnOffBlob_Slime() => m_toggleBlobBugSlime.TurnOffAndPushInteger();

    [ContextMenu("Turn blob off spike")] 
    public void TurnOffBlob_Spike() => m_toggleBugSpike.TurnOffAndPushInteger();

    [ContextMenu("Turn blob off flamme")] 
    public void TurnOffBlob_Flamme() => m_toggleBlobBugFlamme.TurnOffAndPushInteger();

   
}
