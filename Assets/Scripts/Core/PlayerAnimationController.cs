using SLC.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public SpriteRenderer sample;

        public Sprite idle;
        public Sprite walk;
        public Sprite shoot;

        private MovementController movementController;
        private InputHandler handler;

        private void Start()
        {
            handler = GetComponent<InputHandler>();
            movementController = GetComponent<MovementController>();
        }

        private void Update()
        {
            Vector3 mouse = UnityEngine.Input.mousePosition;
            mouse = Camera.main.ScreenToWorldPoint(mouse);

            if (movementController.m_currentSpeed > 0 && !handler.shoot)
            {
                sample.sprite = walk;
            }
            else if (movementController.m_currentSpeed <= 0 && !handler.shoot)
            {
                sample.sprite = idle;
            }
            else if (handler.shoot)
            {
                sample.sprite = shoot;
            }

            if (handler.InputVector.x > 0 && !handler.shoot)
            {
                sample.flipX = false;
            }
            else if (handler.InputVector.x < 0 && !handler.shoot)
            {
                sample.flipX = true;
            }
            else if (handler.shoot && mouse.x > transform.position.x)
            {
                sample.flipX = false;
            }
            else if (handler.shoot && mouse.x < transform.position.x)
            {
                sample.flipX = true;
            }
        }
    }
}