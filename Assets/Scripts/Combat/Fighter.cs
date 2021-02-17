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

        Health target;
        Mover playerMovement;

        private void Awake() {
            playerMovement = GetComponent<Mover>();
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target != null) {
                if (!target.IsAlive()) return;



                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToTarget > weaponRange) {
                    playerMovement.MoveTo(target.transform.position);
                } else
                {
                    
                    playerMovement.Cancel();
                    if(timeSinceLastAttack > timeBetweenAttacks)
                    {
                     AttackBehaviour();
 
                    }
                    
                }


            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            GetComponentInChildren<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0f;              
        }

        public void Attack(CombatTarget combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>(); 
        }

        public void Cancel() {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;            
        }

        // Animation Event
        private void Hit()
        {
            if(target != null) target.TakeDamage(weaponDamage);
        }

    }
}