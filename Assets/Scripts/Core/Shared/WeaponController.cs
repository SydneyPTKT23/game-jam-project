using SLC.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SLC.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        public InputHandler handler;

        public GameObject WeaponRoot;
        public Transform WeaponMuzzle;

        public ProjectileBase ProjectilePrefab;

        public float FireRate = 0.2f;
        public float BulletSpreadAngle = 0;

        public int BulletsPerShot = 1;
        public float RecoilForce = 1.0f;

        public int MaxAmmo = 50;

        public UnityAction OnShoot;

        private int m_CurrentAmmo;
        private float m_NextTimeToFire = Mathf.NegativeInfinity;
        public Vector3 m_LastMuzzlePosition;


        public GameObject Owner { get; set; }
        public GameObject SourcePrefab { get; set; }
        public Vector3 MuzzleWorldVelocity { get; private set; }

        public AudioClip sound;
        public AudioSource src;

        public int GetCurrentAmmo() => Mathf.FloorToInt(m_CurrentAmmo);

        private AudioSource m_audioSource;

        private void Start()
        {
            m_CurrentAmmo = MaxAmmo;
            m_LastMuzzlePosition = WeaponMuzzle.position;

            m_audioSource = GetComponent<AudioSource>();

            Owner = gameObject;
        }

        private void Update()
        {
            if (handler.shoot)
                HandleShoot();

            if (Time.deltaTime > 0)
            {
                MuzzleWorldVelocity = (WeaponMuzzle.position - m_LastMuzzlePosition) / Time.deltaTime;
                m_LastMuzzlePosition = WeaponMuzzle.position;
            }
        }

        public void UseAmmo(int t_amount)
        {
            m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo - t_amount, 0, MaxAmmo);
            m_NextTimeToFire = Time.time;
        }

        private void HandleShoot()
        {
            //m_CurrentAmmo >= 1 && 

            if (Time.time >= m_NextTimeToFire)
            {
                SpawnProjectile();
                m_CurrentAmmo -= 1;

                src.PlayOneShot(sound);

                CameraShake.Instance.ShakeCamera(5f, 0.1f);
                m_NextTimeToFire = Time.time + FireRate;
            }
        }

        public void SpawnProjectile()
        {
            for (int i = 0; i < BulletsPerShot; i++)
            {
                float t_addedOffset = Random.Range(-BulletSpreadAngle / 2, BulletSpreadAngle / 2);

                Quaternion t_newRotation = Quaternion.Euler(WeaponMuzzle.eulerAngles.x, WeaponMuzzle.eulerAngles.y + t_addedOffset, WeaponMuzzle.eulerAngles.z);

                ProjectileBase t_newProjectile = Instantiate(ProjectilePrefab, WeaponMuzzle.position, t_newRotation);
                t_newProjectile.Shoot(this);
            }

            m_NextTimeToFire = Time.time;

            OnShoot?.Invoke();
        }
    }
}
