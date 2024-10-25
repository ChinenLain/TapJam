using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameLogic
{
    public static class Direction
    {
        public enum DirectionType
        {
            Mid = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4
        }

        public static Vector2 GetClosestDirection(Vector2 direction)
        {
            Vector2[] directions = new Vector2[]
            {
            new Vector2(0, 1),   // Up
            new Vector2(1, 0),   // Right
            new Vector2(0, -1),  // Down
            new Vector2(-1, 0)   // Left
            };

            Vector2 closestDirection = directions[0];
            float minAngle = Vector2.Angle(direction, closestDirection);

            for (int i = 1; i < directions.Length; i++)
            {
                float angle = Vector2.Angle(direction, directions[i]);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    closestDirection = directions[i];
                }
            }

            return closestDirection;
        }

        public static int GetClosestDirectionType(Vector2 direction)
        {
            Vector2 v = GetClosestDirection(direction);
            int dir;
            if (v.x == -1) dir = (int)DirectionType.Left;
            else if (v.x == 1) dir = (int)DirectionType.Right;
            else dir = (v.y == 1) ? (int)DirectionType.Up : (int)DirectionType.Down;
            return dir;
        }
    }
}
