using SLC.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class WeaponAimController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;

        #region Private Fields

        private Camera mainCamera;

        #endregion

        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            // Cache the camera, Camera.main is an expensive operation.
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Aim();
        }

        #endregion

        private void Aim()
        {
            (bool success, Vector3 position) = GetMousePosition();
            if (success)
            {
                // Calculate the direction
                var direction = position - transform.position;

                // You might want to delete this line.
                // Ignore the height difference.
                direction.y = 0;

                // Make the transform look in the direction.
                transform.forward = direction;
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            Ray ray = mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, groundMask))
            {
                // The Raycast hit something, return with the position.
                return (success: true, position: hitInfo.point);
            }
            else
            {
                // The Raycast did not hit anything.
                return (success: false, position: Vector3.zero);
            }
        }

        #endregion
    }
}