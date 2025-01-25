using System;
using UnityEngine;

namespace SLC.Core
{
    public class Damageable : MonoBehaviour
    {
        public float damageMultiplier = 1f;
        [Range(0, 100)] public float selfDamageMultiplier = 0.5f;

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
                float t_totalDamage = t_damage;

                if(Health.gameObject == t_damageSource)
                {
                    t_totalDamage *= selfDamageMultiplier;
                }

                int t_finalDamage = Convert.ToInt32(t_totalDamage);
                Health.TakeDamage(t_finalDamage, t_damageSource);
            }
        }
    }
}