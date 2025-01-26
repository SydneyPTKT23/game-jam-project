using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class Bubble : MonoBehaviour
    {
        public float size = 0.1f;

        public Transform sprite;
        public Health health;

        private void Start()
        {
            health = GetComponent<Health>();
            health.OnDamaged += AddBubble;
        }

        private void Update()
        {
            Vector3 scale = new(sprite.localScale.x * size, sprite.localScale.y * size, sprite.localScale.z * size);
            sprite.localScale = scale;
        }

        private void AddBubble(int t_damage, GameObject t_source)
        {
            size += 0.5f;
        }
    }
}
