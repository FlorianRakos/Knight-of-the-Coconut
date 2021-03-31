using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

    Mover playerMovement;
    Health health;

    enum CursorType {
        None,
        Movement,
        Combat     
    }

    [System.Serializable]
    struct CursorMapping {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
        }

    [SerializeField] CursorMapping[] cursorMapping = null;

    void Awake()
    {
        playerMovement = GetComponent<Mover>();
        health = GetComponent<Health>();
    }


    void Update()
        {
            if(!health.IsAlive()) return;
            if(UpdateCombat()) return;            
            if(UpdateMovement()) return;
            SetCursor(CursorType.None);
            //print("cant move here");
        }

        private bool UpdateCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits) {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if(Input.GetMouseButton(0)) {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                SetCursor(CursorType.Combat);
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
                    playerMovement.StartMoveAction(destination, 1f);
                    
                }
                SetCursor(CursorType.Movement);
                return true;

            }
            return false;

        }

        private void SetCursor(CursorType cursorType) {
            CursorMapping cursorMapping = GetCursorMapping(cursorType);
            Cursor.SetCursor(cursorMapping.texture, cursorMapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type) {
            foreach (CursorMapping mapping in cursorMapping)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMapping[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }
    }
