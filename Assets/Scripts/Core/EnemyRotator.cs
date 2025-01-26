using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class EnemyRotator : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (player.position.x > transform.position.x)
            {
                sprite.flipX = true;
            }
            else if (player.position.x < transform.position.x)
            {
                sprite.flipX = false;
            }
        }
    }
}