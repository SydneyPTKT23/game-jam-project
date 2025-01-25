using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class EnemyRunner : MonoBehaviour
    {
        private GameObject player;

        public Animator m_animator;



        private EnemyController m_enemyController;
        private AudioSource m_audioSource;

        private void Start()
        {
            m_enemyController = GetComponent<EnemyController>();

            m_audioSource = GetComponent<AudioSource>();

            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            m_enemyController.SetNavDestination(player.transform.position);
        }

        private void OnDamaged()
        {

        }
    }
}
