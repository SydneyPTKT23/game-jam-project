using UnityEngine;
using UnityEngine.Events;

namespace SLC.Core
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public GameObject Owner { get; private set; }
        public Vector3 InitialPosition { get; private set; }
        public Vector3 InitialDirection { get; private set; }
        public Vector3 InheritedMuzzleVelocity { get; private set; }

        public UnityAction OnShoot;

        public void Shoot(WeaponController t_controller)
        {
            Owner = t_controller.Owner;
            InitialPosition = transform.position;
            InitialDirection = transform.forward;
            InheritedMuzzleVelocity = t_controller.MuzzleWorldVelocity;

            OnShoot?.Invoke();
        }
    }
}