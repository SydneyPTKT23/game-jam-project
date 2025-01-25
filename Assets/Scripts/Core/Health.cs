using UnityEngine;
using UnityEngine.Events;

namespace SLC.Core
{
    public class Health : MonoBehaviour
    {
        [Space, Header("Health Settings")]
        [SerializeField] public int maximumHealth = 50;

        public UnityAction<int, GameObject> OnDamaged;
        public UnityAction<int> OnHealed;
        public UnityAction OnDie;

        public int m_currentHealth;
        public bool m_isDead;

        private void Start()
        {
            m_currentHealth = maximumHealth;
        }

        public void TakeDamage(int t_damage, GameObject t_damageSource)
        {
            if (!m_isDead)
            {
                m_currentHealth -= t_damage;
                m_currentHealth = Mathf.Clamp(m_currentHealth, 0, maximumHealth);

                OnDamaged?.Invoke(t_damage, t_damageSource);

                if (m_currentHealth <= 0)
                {
                    m_currentHealth = 0;
                    HandleDeath();
                }
            }
        }

        public void Kill()
        {
            m_currentHealth = 0;
            OnDamaged?.Invoke(maximumHealth, null);

            HandleDeath();
        }

        private void HandleDeath()
        {
            if (m_isDead) { return; }

            if (m_currentHealth <= 0f)
            {
                m_isDead = true;
                OnDie?.Invoke();
            }
        }
    }
}