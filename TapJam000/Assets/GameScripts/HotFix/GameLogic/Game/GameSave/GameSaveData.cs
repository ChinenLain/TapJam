using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class GameSaveData
    {
        //存档相关信息
        public int SaveId;
        public string SaveName;
        public string SaveCreateTime;
        public string SaveUpdateTime;

        //游戏进度相关信息
        public int GameChapter;

        //角色相关信息
        public float ActorBlood;

        public GameSaveData ShallowCopy()
        {
            return (GameSaveData)MemberwiseClone();
        }
    }
}
