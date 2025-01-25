using SLC.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SLC.Core
{
    public class WeaponController : MonoBehaviour
    {
        public InputHandler handler;

        private int m_currentAmmo;
        private float m_nextTimeToFire = Mathf.NegativeInfinity;

        public ProjectileBase ProjectilePrefab;

        public int bulletsPerShot = 1;
        public float BulletSpreadAngle = 0;

        public float fireRate = 0.2f;

        public UnityAction OnShoot;

        public Vector3 LastMuzzlePosition;

        public GameObject Owner { get; set; }
        public Transform WeaponMuzzle;

        public Vector3 MuzzleWorldVelocity { get; private set; }

        private void Start()
        {
            LastMuzzlePosition = WeaponMuzzle.position;

            Owner = gameObject;
        }

        private void Update()
        {
            if (Time.deltaTime > 0)
            {
                MuzzleWorldVelocity = (WeaponMuzzle.position - LastMuzzlePosition) / Time.deltaTime;
                LastMuzzlePosition = WeaponMuzzle.position;
            }

            if (handler.shoot)
                SpawnProjectile();
        }

        private void HandleShoot()
        {
            Debug.Log("shoot");
            if (m_currentAmmo >= 1.0f && Time.time >= m_nextTimeToFire)
            {
                SpawnProjectile();
                m_nextTimeToFire = Time.time + fireRate;

                m_currentAmmo -= 1;
            }
        }

        public void SpawnProjectile()
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Vector3 t_shotDirection = GetShotDirectionWithinSpread(WeaponMuzzle);
                ProjectileBase t_newProjectile = Instantiate(ProjectilePrefab, WeaponMuzzle.position,
                    Quaternion.LookRotation(t_shotDirection));
                t_newProjectile.Shoot(this);
            }

            OnShoot?.Invoke();
        }

        public Vector3 GetShotDirectionWithinSpread(Transform t_shootTransform)
        {
            float t_spreadAngleRatio = BulletSpreadAngle / 180f;
            Vector3 t_spreadWorldDirection = Vector3.Slerp(t_shootTransform.forward, Random.insideUnitSphere,
                t_spreadAngleRatio);

            return t_spreadWorldDirection;
        }
    }
}
