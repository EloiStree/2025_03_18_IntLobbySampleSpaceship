using UnityEngine;

namespace Eloi.IntAction
{
    public class IntActionMono_EnterSpaceShipMasterCode : DefaultIntegerEmitterEventMono
    {

        public bool m_isCodeSubmitValideOnce = false;
        public IntActionId m_atStartEnteringCode = new IntActionId(430001);
        public IntActionId m_atSubmitWrongCode = new IntActionId(430002);
        public IntActionId m_atSubmitValideCode = new IntActionId(430003);

        [ContextMenu("Notify Entering Code")]
        public void NotifyThatPlayerIsEnteringCode() => SendInteger(m_atStartEnteringCode.Value);

        [ContextMenu("Notify Wrong Code submitted")]
        public void NotifyThatPlayerSubmitWrongCode() => SendInteger(m_atSubmitWrongCode.Value);

        [ContextMenu("Notify Valide Code submitted")]
        public void NotifyThatPlayerSubmitValideCode() {
            m_isCodeSubmitValideOnce = true;
            SendInteger(m_atSubmitValideCode.Value);
        }

        public bool IsCodeSubmitValideOnce() => m_isCodeSubmitValideOnce;
    }


}