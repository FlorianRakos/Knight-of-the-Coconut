using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

    Mover playerMovement;


    void Awake()
    {
        playerMovement = GetComponent<Mover>();

    }


    void Update()
        {
            if(UpdateCombat()) return;            
            if(UpdateMovement()) return;
            print("cant move here");
        }

        private bool UpdateCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if(target == null) continue;

                if(Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }
                    return true;                                
            }
            return false;
        }

        private bool UpdateMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                Vector3 destination = hit.point;
                if (Input.GetMouseButton(0))
                {
                    playerMovement.StartMoveAction(destination);
                    
                }
                return true;

            }
            return false;

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
    }
