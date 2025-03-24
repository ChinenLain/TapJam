using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class SettingData
    {
        public float SoundVolume = 1.0f;
        public float MusicVolume = 1.0f;
        public int joystickType = 0;

        public SettingData ShallowCopy()
        {
            return (SettingData)MemberwiseClone();
        }
    }
}
