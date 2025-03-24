using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public static class UIEventDefine
    {
        public static readonly int StickDrag = RuntimeId.ToRuntimeId("UIEventDefine.StickDrag");
        public static readonly int StickEndDrag = RuntimeId.ToRuntimeId("UIEventDefine.StickEndDrag");
        public static readonly int AttackClick = RuntimeId.ToRuntimeId("UIEventDefine.AttackClick");
        public static readonly int AttackEndClick = RuntimeId.ToRuntimeId("UIEventDefine.AttackEndClick");

        public static readonly int AvatarClick = RuntimeId.ToRuntimeId("UIEventDefine.AvatarClick");

        public static readonly int DialogueClick = RuntimeId.ToRuntimeId("UIEventDefine.DialogueClick");
        public static readonly int PlayTalkFinish = RuntimeId.ToRuntimeId("UIEventDefine.PlayTalkFinish");

        public static readonly int LoadingPanelOpen = RuntimeId.ToRuntimeId("UIEventDefine.LoadingPanelOpen");
        public static readonly int LoadingPanelClose = RuntimeId.ToRuntimeId("UIEventDefine.LoadingPanelClose");

        public static readonly int ActorBloodChanged = RuntimeId.ToRuntimeId("UIEventDefine.ActorBloodChanged");
    }


}
