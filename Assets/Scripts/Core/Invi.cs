using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLC.Core
{
    public class Invi : MonoBehaviour
    {

        private IEnumerator m_IFrameRoutine;

        private Health m_health;

        private void Start()
        {
            m_health = GetComponent<Health>();

            m_health.OnDamaged += OnDamaged;
            m_health.OnDie += Restart;
        }

        private void OnDamaged(int t_damage, GameObject t_damageSource)
        {
            m_health.Invincible = true;

            InvokeIFrameRoutine();
        }

        private void InvokeIFrameRoutine()
        {
            if (m_IFrameRoutine != null)
                StopCoroutine(m_IFrameRoutine);

            m_IFrameRoutine = IFrameRoutine();
            StartCoroutine(m_IFrameRoutine);
        }

        private IEnumerator IFrameRoutine()
        {
            yield return new WaitForSeconds(2);

            m_health.Invincible = false;
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}