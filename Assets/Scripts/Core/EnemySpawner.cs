using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnInterval;

        public float timeAdd = 0.08f;

        public float m_nextSpawn;

        public GameObject spawnPrefab;

        public Transform[] spawnPoints;

        public bool GameRunning = true;

        private void Update()
        {
            SpawnCreature();
        }

        private void SpawnCreature()
        {
            m_nextSpawn += Time.deltaTime;

            if (m_nextSpawn >= spawnInterval)
            {
                Transform t_spawn = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
                Instantiate(spawnPrefab, t_spawn.position, Quaternion.identity);

                m_nextSpawn = 0f;
                spawnInterval -= timeAdd;
            }
        }
    }
}