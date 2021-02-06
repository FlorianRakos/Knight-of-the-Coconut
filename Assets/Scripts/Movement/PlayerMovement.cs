using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        UpdateAnimator();        
    }





    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float speed = localVelocity.z;

        GetComponentInChildren<Animator>().SetFloat("forwardSpeed", speed);

    }
}
