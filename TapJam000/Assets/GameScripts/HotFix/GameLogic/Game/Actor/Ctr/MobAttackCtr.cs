using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class MobAttackCtr : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            transform.parent.GetComponent<MobCtr>().OnWeaponHit(other);
        }
    }
}
