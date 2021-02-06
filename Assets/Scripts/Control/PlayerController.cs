using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    PlayerMovement playerMovement;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            CastRay();
        } 
    }

        private void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit)
        {
            Vector3 destination = hit.point;
            
            playerMovement.MoveTo(destination);
        }

    }
}
