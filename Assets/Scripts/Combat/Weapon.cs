using UnityEngine;

namespace RPG.Combat {

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new Weapons", order = 0)]
    public class Weapon : ScriptableObject {

        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] float weaponDamage = 40f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] bool isRighthanded = true;

        public float GetDamage () {
            return weaponDamage;
        }

        public float GetRange () {
            return weaponRange;
        }

        public float GetAttackCooldown () {
            return timeBetweenAttacks;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) {
            if(equippedPrefab == null) return;

            Transform handTransform;
            if (isRighthanded) handTransform = rightHand;
            else handTransform = leftHand;
            Instantiate(equippedPrefab, handTransform);

            if(animatorOverride == null) return;
            animator.runtimeAnimatorController = animatorOverride;

        }
        
    }
}