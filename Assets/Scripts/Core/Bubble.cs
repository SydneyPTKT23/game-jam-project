using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class Bubble : MonoBehaviour
    {
        public Transform sprite;
        public Health health;

        public Vector3 m_initScale;

        private void Start()
        {
            health = GetComponent<Health>();
            health.OnDamaged += AddBubble;

            m_initScale = sprite.localScale;
        }

        private void AddBubble(int t_damage, GameObject t_source)
        {
            sprite.localScale += new Vector3(.3f, .3f, .3f);
        }
    }
}