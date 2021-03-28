using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
{

    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] bool findsTarget = false;
    [SerializeField] GameObject hitFX;
    [SerializeField] float expiringTimer = .5f;

    float startTime = 0f;

    Health target;
    float damage = 0f;

    private void Start() {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if (target == null) return;
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);

        if(findsTarget && target.IsAlive()) {
            transform.LookAt(GetAimLocation()); 
        }

        startTime += Time.deltaTime;
        if(startTime >= expiringTimer) Destroy(gameObject);

    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
        if (targetCollider == null) return target.transform.position;

        return target.transform.position + new Vector3(0f, targetCollider.height / 2f, 0f);
    }

    public void SetTarget (Health target, float damage) {

        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other) {
        //print("projectile collision with" + other.name);
        if(other.GetComponent<Health>()!= null) {
            other.GetComponent<Health>().TakeDamage(damage);
            
            if (hitFX != null) {
                GameObject impact = Instantiate(hitFX, transform.position, Quaternion.identity);
                Destroy(impact, 5f);
        } 
        }

        
        Destroy(gameObject);

    }

    }
}
