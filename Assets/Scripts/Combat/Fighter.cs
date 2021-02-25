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

        float timeSinceLastAttack = Mathf.Infinity;

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
                    playerMovement.MoveTo(target.transform.position, 1f);
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
            TriggerAttack();
            timeSinceLastAttack = 0f;
        }

        private void TriggerAttack()
        {
            GetComponentInChildren<Animator>().ResetTrigger("stopAttack");
            GetComponentInChildren<Animator>().SetTrigger("attack");
        }

        public bool CanAttack(GameObject combatTarget){
            if (combatTarget == null) return false;
            Health targetToCheck = combatTarget.GetComponent<Health>();

            // bool test = targetToCheck != null && targetToCheck.IsAlive();
            // print(combatTarget);
            // print(test + "can attack");

            return targetToCheck != null && targetToCheck.IsAlive();
        }

        public void Attack(GameObject combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>(); 
        }

        public void Cancel()
        {
            StopAttack();

            print("attack canceld");
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Animation Event
        private void Hit()
        {
            if(target != null) target.TakeDamage(weaponDamage);
        }

    }
}