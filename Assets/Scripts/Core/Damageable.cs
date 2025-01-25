using UnityEngine;

namespace SLC.Core
{
    public class Damageable : MonoBehaviour
    {
        public float damageMultiplier = 1f;
        public Health Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            if (!Health)
            {
                Health = GetComponentInParent<Health>();
            }
        }

        public void InflictDamage(int t_damage, GameObject t_damageSource)
        {
            if (Health)
            {
                Health.TakeDamage(t_damage, t_damageSource);
            }
        }
    }
}