using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public static class ChapterMapDefine
    {
        public static readonly MapInfo[] chapter_00 = new MapInfo[19]
        {
            new MapInfo(1,(int)MapType.ROCK_CROSS,10,2,18,4),
            new MapInfo(2,(int)MapType.ROCK_CROSS,1,3,-1,5),
            new MapInfo(3,(int)MapType.ROCK_CROSS,2,-1,-1,6),
            new MapInfo(4,(int)MapType.ROCK_CROSS,-1,5,1,7),
            new MapInfo(5,(int)MapType.ROCK_CROSS,4,6,2,8),
            new MapInfo(6,(int)MapType.ROCK_CROSS,5,-1,3,9),
            new MapInfo(7,(int)MapType.ROCK_CROSS,-1,8,4,-1),
            new MapInfo(8,(int)MapType.ROCK_CROSS,7,9,5,-1),
            new MapInfo(9,(int)MapType.ROCK_CROSS,8,-1,6,-1),


            new MapInfo(10,(int)MapType.FOREST_RECT01  ,-1,11,-1,-1),
            new MapInfo(11,(int)MapType.FOREST_RECT02  ,10,12,-1,-1),
            new MapInfo(12,(int)MapType.FOREST_FOG     ,11,13,-1,-1),
            new MapInfo(13,(int)MapType.MOUNTAIN_RECT01,12,14,-1,-1),
            new MapInfo(14,(int)MapType.MOUNTAIN_RECT02,13,15,6,-1),
            new MapInfo(15,(int)MapType.MOUNTAIN_FOG   ,14,16,-1,-1),
            new MapInfo(16,(int)MapType.MOUNTAIN_LONG  ,15,17,-1,-1),
            new MapInfo(17,(int)MapType.ROCK_RECT      ,16,18,-1,-1),
            new MapInfo(18,(int)MapType.ROCK_CROSS     ,17,19,2,5),
            new MapInfo(19,(int)MapType.GRASS_RECT     ,18,10,-1,-1)
        };

        public static readonly MapInfo[] chapter_01 = new MapInfo[5]
        {
            new MapInfo(1,(int)MapType.FOREST_RECT01  ,-1,2,-1,-1),
            new MapInfo(2,(int)MapType.GRASS_RECT     ,1,3,-1,-1),
            new MapInfo(3,(int)MapType.FOREST_RECT01  ,2,4,-1,-1),
            new MapInfo(4,(int)MapType.MOUNTAIN_RECT01,3,5,-1,-1),
            new MapInfo(5,(int)MapType.MOUNTAIN_RECT02,4,-1,-1,-1),
        };

        public static readonly MapInfo[] chapter_02 = new MapInfo[10]
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
