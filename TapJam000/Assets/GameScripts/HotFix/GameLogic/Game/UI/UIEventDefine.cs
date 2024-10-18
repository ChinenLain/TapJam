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
    }
}
