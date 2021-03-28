using System;
using RPG.Core;
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
        [SerializeField] Projectile projectile;

        const string weaponName = "Weapon";


        public float GetDamage () {
            return weaponDamage;
        }

        public float GetRange () {
            return weaponRange;
        }

        public float GetAttackCooldown () {
            return timeBetweenAttacks;
        }

        public bool HasProjectile () {
            return projectile !=null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab != null) {

                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;                
            }


            if (animatorOverride == null) return;
            Debug.Log(animator.runtimeAnimatorController);
            animator.runtimeAnimatorController = animatorOverride;

        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);

            if(oldWeapon != null) {
                oldWeapon.gameObject.name = "DESTROYING";                    
                Destroy(oldWeapon.gameObject);
                }

            oldWeapon = leftHand.Find(weaponName);

            if(oldWeapon != null) {
                oldWeapon.gameObject.name = "DESTROYING";                
                Destroy(oldWeapon.gameObject);
            }
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRighthanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }
    }
}