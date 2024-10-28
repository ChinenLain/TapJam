using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class WeaponCallBack : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            transform.parent.Find("WeaponRoot").GetComponentInParent<WeaponRoot>().OnWeaponHit(other);
        }
    }
}
