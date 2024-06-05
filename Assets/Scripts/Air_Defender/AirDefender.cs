using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDefender : MonoBehaviour
{
    [Header("Missile Settings")]
    public float detectionRadius = 50f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    
    [Header("Current Targets")]
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    
    private float _fireCooldown;


    private IEnumerator DetectTargets()
    {
        while (true)
        {
            DetectAndFire();
            yield return new WaitForSeconds(0.1f); 
        }
    }

    private void Update()
    {
        DetectAndFire();
    }

    private void DetectAndFire()
    {
        if (_fireCooldown > 0)
        {
            _fireCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            targets.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position)));
            GameObject nearestTarget = targets[0];

            FireAtTarget(nearestTarget);
        }
    }

    private void FireAtTarget(GameObject target)
    {
        if (target == null) return;

        Vector3 direction = (target.transform.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        projectile.GetComponent<Missile>().SetTarget(target.transform);
        projectile.GetComponent<Missile>().AddForce(50);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            targets.Remove(other.gameObject);
        }
    }
}
