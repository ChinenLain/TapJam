using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public static class GameEventDefine
    {
        public static readonly int ChangeMap = RuntimeId.ToRuntimeId("GameEventDefine.ChangeMap");
        public static readonly int LoadMapFinish = RuntimeId.ToRuntimeId("GameEventDefine.LoadMapFinish");

        public static readonly int DialogueTaskStart = RuntimeId.ToRuntimeId("GameEventDefine.DialogueTaskStart");
        public static readonly int DialogueTaskFinish = RuntimeId.ToRuntimeId("GameEventDefine.DialogueTaskFinish");
    }
}
