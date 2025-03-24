using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class WeaponRoot : MonoBehaviour
    {
        public WeaponType weaponType;
        private Animator animator;
        public GameObject weapon;
        private float hurt = 10.0f;
        void Start()
        {
            animator = GetComponent<Animator>();
            weapon = transform.parent.Find("weapon").gameObject;
            weapon.SetActive(false);
        }


        public void SetState(string state,int dir)
        {
            string statename = "we-" + state;
            weapon.SetActive(false);
            switch ((Direction.DirectionType)dir)
            {
                case Direction.DirectionType.Mid:
                    statename = "we-stand";
                    transform.localPosition = new Vector3(0,-2.5f,-0.01f);
                    animator.Play(statename);
                    break;
                case Direction.DirectionType.Left:
                    statename += "left";
                    animator.Play(statename);
                    transform.localPosition = new Vector3(0, 0, 0.01f);
                    break;
                case Direction.DirectionType.Right:
                    statename += "right";
                    animator.Play(statename);
                    transform.localPosition = new Vector3(0, 0, 0.01f);
                    break;
                case Direction.DirectionType.Up:
                    statename += "up";
                    animator.Play(statename);
                    transform.localPosition = new Vector3(0, 0, -0.01f);
                    break;
                case Direction.DirectionType.Down:
                    statename += "down";
                    animator.Play(statename);
                    if(state == "move")
                    {
                        transform.localPosition = new Vector3(0, -2.5f, 0.01f);
                    }
                    else
                    {
                        transform.localPosition = new Vector3(0, 0, 0.01f);
                    }
                    break;
            }
            if (state == "hit")
            {
                SetAttack(dir);
            }
        }

        private void SetAttack(int dir)
        {
            weapon.SetActive(true);
            switch ((Direction.DirectionType)dir)
            {
                case Direction.DirectionType.Mid:
                    weapon.SetActive(false);
                    break;
                case Direction.DirectionType.Left:
                    weapon.transform.localRotation = Quaternion.Euler(0f, 90.0f, 0f);
                    break;
                case Direction.DirectionType.Right:
                    weapon.transform.localRotation = Quaternion.Euler(0f, -90.0f, 0f);
                    break;
                case Direction.DirectionType.Down:
                    weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case Direction.DirectionType.Up:
                    weapon.transform.localRotation = Quaternion.Euler(0f, 180.0f, 0f);
                    break;
            }
        }

        public void OnWeaponHit(Collider other)
        {
            if (other.gameObject.CompareTag("Mob"))
            {
                other.gameObject.GetComponent<MobCtr>().HitBy(hurt);
            }
        }
    }

    public enum WeaponType
    {
        NONE = 0,
        SCYTHE = 1
    }
}
