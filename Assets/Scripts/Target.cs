using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Missile"))
            {
                PoolManager.Instance.ReturnPool(this.gameObject);
            }
        }
    }
}