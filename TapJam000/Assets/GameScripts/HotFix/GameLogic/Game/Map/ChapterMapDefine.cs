using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public static class ChapterMapDefine
    {
        public static readonly MapInfo[] chapter_01 = new MapInfo[10]
        {
            new MapInfo(0,(int)MapType.FOREST_RECT01  ,-1,1,-1,-1),
            new MapInfo(1,(int)MapType.FOREST_RECT02  ,0,2,-1,-1),
            new MapInfo(2,(int)MapType.FOREST_FOG     ,1,3,-1,-1),
            new MapInfo(3,(int)MapType.MOUNTAIN_RECT01,2,4,-1,-1),
            new MapInfo(4,(int)MapType.MOUNTAIN_RECT02,3,5,6,-1),
            new MapInfo(5,(int)MapType.MOUNTAIN_FOG   ,4,6,-1,-1),
            new MapInfo(6,(int)MapType.MOUNTAIN_LONG  ,5,7,-1,-1),
            new MapInfo(7,(int)MapType.ROCK_RECT      ,6,8,-1,-1),
            new MapInfo(8,(int)MapType.ROCK_CROSS     ,7,9,2,5),
            new MapInfo(9,(int)MapType.GRASS_RECT     ,8,0,-1,-1)
        };

        public static List<MapInfo> GetMapInfo(MapInfo[] name)
        {
            List<MapInfo> mapInfos = new List<MapInfo>();
            for(int i = 0; i < name.Length; i++)
            {
                mapInfos.Add(name[i]);
            }
            return mapInfos;
        }
    }

    public enum MapType
    {
        FOREST_RECT01 = 0,
        FOREST_RECT02 = 1,
        FOREST_FOG = 2,

        MOUNTAIN_RECT01 = 3,
        MOUNTAIN_RECT02 = 4,
        MOUNTAIN_FOG = 5,

        MOUNTAIN_LONG = 6,

        ROCK_RECT = 7,

        ROCK_CROSS = 8,

        GRASS_RECT = 9
    }

    public class MapInfo
    {
        public int id;
        public int maptype;
        public int left_id;
        public int right_id;
        public int up_id;
        public int down_id;

        public MapInfo(int id, int mapObject, int left_id, int right_id, int up_id, int down_id)
        {
            this.id = id;
            this.maptype = mapObject;
            this.left_id = left_id;
            this.right_id = right_id;
            this.up_id = up_id;
            this.down_id = down_id;
        }
    }
}
