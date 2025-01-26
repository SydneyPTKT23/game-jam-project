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

        public int CurrentHealth;
        public bool Invincible { get; set; }
        public bool m_isDead;

        [SerializeField] private ResourceBarTracker healthBar;

        private void Start()
        {
            CurrentHealth = maximumHealth;

            healthBar.Setup(
                CurrentHealth,
                maximumHealth,
                1000,
                false,
                ResourceBarTracker.ShapeType.RectangleHorizontal,
                ResourceBarTracker.DisplayType.LongValue,
                false,
                null
                );
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

                healthBar.ChangeResourceByAmount(-t_trueDamage);
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