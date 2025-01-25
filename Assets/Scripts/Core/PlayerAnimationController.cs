using SLC.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLC.Core
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public SpriteRenderer sample;

        private MovementController movementController;
        private InputHandler handler;


        [SerializeField] private Animator m_animator;

        private readonly int HorizontalHash = Animator.StringToHash("xVelocity");
        private readonly int VerticalHash = Animator.StringToHash("yVelocity");

        private readonly int move = Animator.StringToHash("move");
        private readonly int shooting = Animator.StringToHash("shoot");

        bool facingRight = true;

        private void Start()
        {
            handler = GetComponent<InputHandler>();
            movementController = GetComponent<MovementController>();
        }

        private void Update()
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);


            m_animator.SetFloat(HorizontalHash, handler.InputVector.x);
            m_animator.SetFloat(VerticalHash, handler.InputVector.y);

            m_animator.SetBool(move, handler.InputVector != Vector2.zero);
            m_animator.SetBool(shooting, handler.shoot);

            if (handler.InputVector.x < 0 && !handler.shoot && facingRight)
            {
                flip();
                sample.flipX = true;
            }
            else if (handler.InputVector.x > 0 && !handler.shoot && !facingRight)
            {
                flip();
                sample.flipX = false;
            }
        }

        private void flip()
        {
            facingRight = !facingRight;
        }
    }
}