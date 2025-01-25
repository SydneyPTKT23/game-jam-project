using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class EnemyMelee : MonoBehaviour
    {
        [SerializeField, Min(1)] private int damage = 1;
        [SerializeField, Min(0.25f)] private float cooldown = 0.25f;

        private float currentCooldown;

        private void Update()
        {
            currentCooldown -= Time.deltaTime;
        }

        private void OnTriggerEnter(Collider t_collider)
        {
            if (currentCooldown > 0f)
                return;

            DealDamageToTarget(t_collider.gameObject);
        }

        private void OnTriggerStay(Collider t_collider)
        {
            if (currentCooldown > 0f)
                return;

            DealDamageToTarget(t_collider.gameObject);
        }

        private void DealDamageToTarget(GameObject t_target)
        {
            if (t_target.GetComponent<EnemyController>())
                return;

            Damageable t_damageable = t_target.GetComponent<Damageable>();
            if (t_damageable)
            {
                t_damageable.InflictDamage(damage, gameObject);
            }

            currentCooldown = cooldown;
        }
    }
}