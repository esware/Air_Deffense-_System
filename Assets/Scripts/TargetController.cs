using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class TargetController : MonoBehaviour
    {
        [Header("Enemy Settings")]
        public float radius;
        public Vector3 center;
        public int frequency;
        public Transform firePoint;

        public Vector3 targetPosition = Vector3.zero;
        public float jumpPower = 2f;
        public float duration = 2f;

        [Header("Movement Settings")]
        [Range(0,1)]
        public float speed;
        public float angle;

        private Vector3 direction=Vector3.left;

        private void Start()
        {
            StartCoroutine(nameof(CreateTarget));
        }

        private IEnumerator CreateTarget()
        {
            while (true)
            {
                var target = PoolManager.Instance.GetTarget();

                target.transform.position = firePoint.position;

                EnemyFire(target);

                yield return new WaitForSecondsRealtime(frequency);
            }
        }

        private void EnemyFire(GameObject target)
        {
            target.transform.DOJump(targetPosition, jumpPower, 1, duration).SetEase(Ease.Linear);
        }

        private void Update()
        {
            angle += speed * Time.deltaTime;

 
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

 
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}