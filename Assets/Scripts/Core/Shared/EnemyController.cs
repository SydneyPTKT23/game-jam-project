using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace SLC.Core
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        public float selfDestructYHeight = -20.0f;

        public float deathDuration = 0f;


        public UnityAction onDamaged;

        public GameObject prefab;

        public NavMeshAgent NavMeshAgent { get; private set; }


        private EnemyManager m_enemyManager;

        private Health m_health;

        private Collider[] m_selfColliders;

        EnemySpawner spawner;

        private void Start()
        {
            m_enemyManager = FindObjectOfType<EnemyManager>();

            m_enemyManager.RegisterEnemy(this);

            m_health = GetComponent<Health>();


            NavMeshAgent = GetComponent<NavMeshAgent>();
            m_selfColliders = GetComponentsInChildren<Collider>();

            m_health.OnDie += OnDie;
            m_health.OnDamaged += OnDamaged;

            spawner = FindObjectOfType<EnemySpawner>();
        }

        public void SetNavDestination(Vector3 t_destination)
        {
            if (NavMeshAgent)
            {
                NavMeshAgent.SetDestination(t_destination);
            }
        }

        private void OnDamaged(int t_damage, GameObject t_damageSource)
        {
            if (t_damageSource && !t_damageSource.GetComponent<EnemyController>())
            {

                onDamaged?.Invoke();
            }
        }

        private void OnDie()
        {
            GameObject t_instance = Instantiate(prefab, transform.position, Quaternion.identity);
            m_enemyManager.UnregisterEnemy(this);

            spawner.creatures.Remove(gameObject);
            Destroy(gameObject, deathDuration);
        }
    }
}
