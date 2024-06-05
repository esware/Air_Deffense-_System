using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MissileControl
{
    public class MissileCollisionAvoidance : MonoBehaviour
    {
        public float collisionAvoidanceRadius = 5f;
        public LayerMask missileLayer;

        void Update()
        {
            /*Collider[] colliders = Physics.OverlapSphere(transform.position, collisionAvoidanceRadius, missileLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    Vector3 avoidanceDirection = transform.position - collider.transform.position;
                    transform.position += avoidanceDirection.normalized * Time.deltaTime;
                }*
            }*/
        }

        void OnDrawGizmos()
        {
            // Çarpışma önleme yarıçapını çiz
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, collisionAvoidanceRadius);

            // Çarpışma önleme yarıçapı içinde bulunan diğer füzeleri çiz
            Collider[] colliders = Physics.OverlapSphere(transform.position, collisionAvoidanceRadius, missileLayer);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, collider.transform.position);
                }
            }
        }
    }

}