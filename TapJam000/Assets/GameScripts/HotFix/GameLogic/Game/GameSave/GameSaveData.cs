using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class GameSaveData
    {
        //�浵�����Ϣ
        public int SaveId;
        public string SaveName;
        public string SaveCreateTime;
        public string SaveUpdateTime;

        //��Ϸ���������Ϣ
        public int GameChapter;

        //��ɫ�����Ϣ
        public float ActorBlood;

        public GameSaveData ShallowCopy()
        {
            return (GameSaveData)MemberwiseClone();
        }
    }
}
