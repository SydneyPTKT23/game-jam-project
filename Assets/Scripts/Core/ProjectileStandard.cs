using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class ProjectileStandard : ProjectileBase
    {
        public Transform root;

        public float speed = 10.0f;

        public float radius = 0.5f;

        public bool inheritWeaponVelocity = false;

        public int damage = 10;

        public LayerMask hittableLayers = -1;

        private ProjectileBase m_projectileBase;
        private Vector3 m_lastRootPosition;
        private Vector3 m_velocity;
        private float m_shootTime;
        private List<Collider> m_ignoredColliders;

        private const QueryTriggerInteraction k_triggerInteraction = QueryTriggerInteraction.Collide;

        private void OnEnable()
        {
            m_projectileBase = GetComponent<ProjectileBase>();
            m_projectileBase.OnShoot += OnShoot;

        }

        new void OnShoot()
        {
            m_shootTime = Time.time;
            m_lastRootPosition = root.position;
            m_velocity = transform.forward * speed;
            m_ignoredColliders = new List<Collider>();
            transform.position += m_projectileBase.InheritedMuzzleVelocity * Time.deltaTime;

            // Ignore colliders of owner
            Collider[] t_ownerColliders = m_projectileBase.Owner.GetComponentsInChildren<Collider>();
            m_ignoredColliders.AddRange(t_ownerColliders);

        }

        private void Update()
        {
            transform.position += m_velocity * Time.deltaTime;
            if (inheritWeaponVelocity)
            {
                transform.position += m_projectileBase.InheritedMuzzleVelocity * Time.deltaTime;
            }

            transform.forward = m_velocity.normalized;


            // Hit detection
            {
                RaycastHit t_closestHit = new();
                t_closestHit.distance = Mathf.Infinity;
                bool t_foundHit = false;

                Vector3 t_movedInLastFrame = root.position - m_lastRootPosition;
                RaycastHit[] t_hits = Physics.SphereCastAll(m_lastRootPosition, radius,
                    t_movedInLastFrame.normalized, t_movedInLastFrame.magnitude, hittableLayers,
                    k_triggerInteraction);

                foreach (RaycastHit t_hit in t_hits)
                {
                    if (IsHitValid(t_hit) && t_hit.distance < t_closestHit.distance)
                    {
                        t_foundHit = true;
                        t_closestHit = t_hit;
                    }
                }

                if (t_foundHit)
                {
                    // Handle case of casting while already inside a collider
                    if (t_closestHit.distance <= 0f)
                    {
                        t_closestHit.point = root.position;
                        t_closestHit.normal = -transform.forward;
                    }

                    OnHit(t_closestHit.point, t_closestHit.normal, t_closestHit.collider);
                }
            }

            m_lastRootPosition = root.position;
        }

        bool IsHitValid(RaycastHit t_hit)
        {
            /*
            // Ignore hits with an ignore component
            if (t_hit.collider.GetComponent<IgnoreHitDetection>())
            {
                return false;
            }

            // Ignore hits with triggers that don't have a Damageable component
            if (t_hit.collider.isTrigger && t_hit.collider.GetComponent<Damageable>() == null)
            {
                return false;
            }*/

            // Ignore hits with specific ignored colliders (self colliders, by default)
            if (m_ignoredColliders != null && m_ignoredColliders.Contains(t_hit.collider))
            {
                return false;
            }

            return true;
        }

        private void OnHit(Vector3 t_point, Vector3 t_normal, Collider t_collider)
        {
            Damageable t_damageable = t_collider.GetComponent<Damageable>();
            if (t_damageable)
            {
                t_damageable.InflictDamage(damage, m_projectileBase.Owner);
            }

            Destroy(gameObject);
        }
    }
}