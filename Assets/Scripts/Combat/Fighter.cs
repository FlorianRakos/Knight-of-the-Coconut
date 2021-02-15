using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 40f;
        [SerializeField] float timeBetweenAttacks = 1f;

        float timeSinceLastAttack = 0f;

        Transform target;
        PlayerMovement playerMovement;

        private void Awake() {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target != null) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (distanceToTarget > weaponRange) {
                    playerMovement.MoveTo(target.position);
                } else
                {
                    playerMovement.Cancel();
                    if(timeSinceLastAttack > timeBetweenAttacks)
                    {
                     AttackBehaviour();
                     timeSinceLastAttack = 0f;   
                    }
                    
                }


            }
        }

        private void AttackBehaviour()
        {

            GetComponentInChildren<Animator>().SetTrigger("attack");
        }

        public void Attack(CombatTarget combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform; 
        }

        public void Cancel() {
            target = null;
        }

        // Animation Event
        private void Hit()
        {
            if(target != null) target.GetComponent<Health>().TakeDamage(weaponDamage);
        }

    }
}