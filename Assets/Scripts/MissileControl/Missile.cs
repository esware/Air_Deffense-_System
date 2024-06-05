using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;


public class Missile : MonoBehaviour
{
    private Transform _target;
    private Transform _closestMissile;
    
    public float speed = 20f;
    public float rotateSpeed = 200f;
    public float perceptionRadius = 50f;
    public float maxForce = 0.8f;
    public float maxSpeed = 5f;

    public void SetTarget(Transform target)
    {
        GetComponent<SphereCollider>().radius = perceptionRadius;
        _target = target;
        
        //transform.DOJump(target.transform.position, 5, 1, speed * Time.deltaTime).SetEase(Ease.InSine);
        //StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        while (_target != null)
        {
            Vector3 direction = Cohesion();

            transform.position += (direction * (speed * Time.deltaTime));

            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 Cohesion()
    {
        Vector3 targetPosition = _target.transform.position;
        Vector3 direction = (targetPosition - transform.position);

        return direction.normalized;
    }
    public void AddForce(float forceAmount)
    {
        Vector3 forceDirection = Cohesion();
        GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Missile"))
        {
            _closestMissile = other.gameObject.transform;
        }

        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Missile"))
        {
            _closestMissile = null;
        }
    }
}