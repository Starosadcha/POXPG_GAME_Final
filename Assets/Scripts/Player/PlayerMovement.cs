using System;
using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class PlayerMovement : MonoBehaviour {
        private static readonly int XMovement = Animator.StringToHash("X_movement");
        private static readonly int YMovement = Animator.StringToHash("Y_movement");
        
        // Konfiguracja prędkości ruchu i biegu
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpForce = 10f;

        // Komponenty z widoku Inspector
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        // Flagi stanu gracza
        private bool _isGrounded;
        
        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() {
            // Ruch poziomy
            Move();

            // Sprawdzenie, czy gracz dotyka ziemi, aby umożliwić skakanie
            CheckGrounded();

            // Skakanie
            if (Input.GetButtonDown("Jump") && _isGrounded) {
                Jump();
            }

            UpdateAnimations();
        }

        private void Move() {
            // Odczyt osi ruchu poziomego
            float moveInput = Input.GetAxis("Horizontal");

            // Przemieszczenie gracza
            _rigidbody2D.velocity = new Vector2(moveInput * speed, _rigidbody2D.velocity.y);
            
            Flip(moveInput);
        }

        private void Jump() {
            // Dodanie siły skoku w osi Y
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
        }

        private void CheckGrounded() {
            // Sprawdzanie, czy gracz dotyka ziemi
            _isGrounded = Physics2D.IsTouchingLayers(_collider2D, LayerMask.GetMask("Default"));
        }

        private void UpdateAnimations() {
            _animator.SetFloat(XMovement, Math.Abs(_rigidbody2D.velocity.x));
            _animator.SetFloat(YMovement, Math.Abs(_rigidbody2D.velocity.y));
        }

        private void Flip(float moveInput) {
            if (moveInput > 0) {
                _spriteRenderer.flipX = false;
            } else if (moveInput < 0) {
                _spriteRenderer.flipX = true;
            }
        }
    }
}