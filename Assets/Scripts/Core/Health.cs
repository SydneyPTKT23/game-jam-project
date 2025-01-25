using UnityEngine;
using UnityEngine.Events;

namespace SLC.Core
{
    public class Health : MonoBehaviour
    {
        [Space, Header("Health Settings")]
        [SerializeField] private int maximumHealth = 50;

        public UnityAction<int, GameObject> OnDamaged;
        public UnityAction<int> OnHealed;
        public UnityAction OnDie;

        public int CurrentHealth { get; set; }
        public bool Invincible { get; set; }
        public bool m_isDead;

        private void Start()
        {
            CurrentHealth = maximumHealth;
        }

        public void TakeDamage(int t_damage, GameObject t_damageSource)
        {
            if (Invincible)
                return;

            int t_healthBefore = CurrentHealth;
            CurrentHealth -= t_damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maximumHealth);

            int t_trueDamage = t_healthBefore - CurrentHealth;
            if (t_trueDamage > 0)
            {
                OnDamaged?.Invoke(t_trueDamage, t_damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0;
            OnDamaged?.Invoke(maximumHealth, null);

            HandleDeath();
        }

        private void HandleDeath()
        {
            if (m_isDead)
                return;

            if (CurrentHealth <= 0f)
            {
                m_isDead = true;
                OnDie?.Invoke();
            }
        }
    }
}