using System;
using System.Linq;
using TEngine;

namespace GameLogic
{

    public class GameSaveManager : BehaviourSingleton<GameSaveManager>
    {
        /// <summary>
        /// 最大存档数量
        /// </summary>
        public static readonly int MAX_SAVE = 3;

        /// <summary>
        /// 上次退出时正在游玩的存档
        /// </summary>
        public int CurrentSaveId
        {
            get => GameModule.Setting.HasSetting("current_save_id") ?
                GameModule.Setting.GetInt("current_save_id") : -1;

            set => GameModule.Setting.SetInt("current_save_id", value);
        }

        /// <summary>
        /// 游戏设置存档
        /// </summary>
        public SettingData settingData
        {
            get
            {
                if (!GameModule.Setting.HasSetting("setting"))
                { GameModule.Setting.SetObject("setting", new SettingData());
                }
                return GameModule.Setting.GetObject<SettingData>("setting").ShallowCopy();
            }

            set => GameModule.Setting.SetObject("setting", value);
        }

        /// <summary>
        /// 所有的游戏存档
        /// </summary>
        private GameSaveData[] m_gameSaveData;

        public int SaveCount => m_gameSaveData?.Count(t => t != null) ?? 0;


        public override void Start()
        {
            m_gameSaveData = new GameSaveData[MAX_SAVE];
            LoadAllGameSaves();
        }

        public void LoadAllGameSaves()
        {
            string[] allSettingName = GameModule.Setting.GetAllSettingNames();

            foreach (string settingName in allSettingName)
            {
                string[] gameSave = settingName.Split('_');
                if(gameSave[0] != "save") continue;
                int saveId = int.Parse(gameSave[1].TrimStart('0'));
                if (saveId >=0 && saveId < MAX_SAVE)
                {
                    m_gameSaveData[saveId] = GameModule.Setting.GetObject<GameSaveData>(settingName);
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of bounds of the list");
                }
            }
        }

        public void CreateGameSave(int saveId)
        {
            if(saveId < 0 || saveId >= MAX_SAVE)
            {
                throw new Exception($"Failure: saveId{saveId} is out of bounds of the MAX_SAVE");
            }
            GameSaveData tmp = new GameSaveData
            {
                SaveId = saveId,
                SaveName = $"存档{saveId}",
                SaveCreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                SaveUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            m_gameSaveData[saveId] = tmp;
            GameModule.Setting.SetObject($"save_{saveId}",tmp);
        }

        public GameSaveData OpenGameSave(int saveId)
        {
            GameSaveData obj = GetGameSave(saveId);
            if(obj != null) CurrentSaveId = saveId;
            return obj;
        }

        public GameSaveData GetGameSave(int saveId)
        {
            if (saveId < 0 || saveId >= MAX_SAVE)
            {
                throw new Exception($"Failure: saveId{saveId} is out of bounds of the MAX_SAVE");
            }
            if (m_gameSaveData[saveId] == null) return null;
            return m_gameSaveData[saveId].ShallowCopy();
        }

        public void SetGameSave(int saveId,GameSaveData gameSaveData)
        {
            if (saveId < 0 || saveId >= MAX_SAVE)
            {
                throw new Exception($"Failure: saveId{saveId} is out of bounds of the MAX_SAVE");
            }
            gameSaveData.SaveUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            m_gameSaveData[saveId] = gameSaveData;
            GameModule.Setting.SetObject($"save_{saveId}", gameSaveData);
        }

        public void CopyGameSaveTo(int saveid_from,int saveid_to)
        {
            m_gameSaveData[saveid_to] = m_gameSaveData[saveid_from].ShallowCopy();
        }
    }
}
