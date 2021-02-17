using UnityEngine;

namespace RPG.Combat
{
    
    
    public class Health : MonoBehaviour {
        [SerializeField] float healthPoints = 100f;

        bool isAlive = true;

        public bool IsAlive(){
            return isAlive;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);
            if (healthPoints <= 0f && isAlive) DyingBehaviour();
        }

        private void DyingBehaviour()
        {
            isAlive = false;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}