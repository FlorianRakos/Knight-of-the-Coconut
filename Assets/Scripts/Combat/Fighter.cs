using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;

        float timeSinceLastAttack = Mathf.Infinity;

        Health target;
        Mover playerMovement;
        Weapon currentWeapon;

        private void Awake() {
            playerMovement = GetComponent<Mover>();
        }

        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;            
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
            
        }

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target != null) {
                if (!target.IsAlive()) return;



                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToTarget > currentWeapon.GetRange()) {
                    playerMovement.MoveTo(target.transform.position, 1f);
                } else
                {
                    
                    playerMovement.Cancel();
                    if(timeSinceLastAttack > currentWeapon.GetAttackCooldown())
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
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        // Animation Event
        private void Hit()
        {
            if(target == null) return;
            if(currentWeapon.HasProjectile()) currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            else target.TakeDamage(currentWeapon.GetDamage());
            
        }

        private void Shoot() 
        {
            Hit();
            //print("shoot event");
        }

    }
}