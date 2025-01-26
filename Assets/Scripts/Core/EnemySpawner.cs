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

        public int creatureCap = 50;
        public List<GameObject> creatures;

        public GameObject spawnPrefab;

        public Transform[] spawnPoints;

        public bool GameRunning = true;

        private void Update()
        {
            if (creatures.Count < creatureCap)
            {
                SpawnCreature();
            }          
        }

        private void SpawnCreature()
        {
            m_nextSpawn += Time.deltaTime;

            if (m_nextSpawn >= spawnInterval)
            {
                Transform t_spawn = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
                GameObject t_sus = Instantiate(spawnPrefab, t_spawn.position, Quaternion.identity);
                creatures.Add(t_sus);


                m_nextSpawn = 0f;
                spawnInterval -= timeAdd;
            }
        }
    }
}