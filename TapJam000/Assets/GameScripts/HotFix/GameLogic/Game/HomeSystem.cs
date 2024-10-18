using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    [Update]
    public class HomeSystem : BehaviourSingleton<HomeSystem>
    {
        public async UniTaskVoid LoadHome()
        {
            await UniTask.Yield();

            GameModule.UI.ShowUIAsync<UIStartWindow>();
        }


    }
}
