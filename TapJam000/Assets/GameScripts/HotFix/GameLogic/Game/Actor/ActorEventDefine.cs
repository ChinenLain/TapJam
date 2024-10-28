using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public static class ActorEventDefine
    {
        public static readonly int Move = RuntimeId.ToRuntimeId("ActorEventDefine.Move");
        public static readonly int Hurt = RuntimeId.ToRuntimeId("ActorEventDefine.Hurt");
        public static readonly int HurtBy = RuntimeId.ToRuntimeId("ActorEventDefine.HurtBy");
        public static readonly int Dead = RuntimeId.ToRuntimeId("ActorEventDefine.Dead");
    }
}
