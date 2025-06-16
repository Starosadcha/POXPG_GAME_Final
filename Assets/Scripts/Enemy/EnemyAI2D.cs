using System.Collections;
using UnityEngine;

namespace Enemy {
    public class EnemyAI2D : MonoBehaviour {
        private static readonly int DieAnimation = Animator.StringToHash("Die");

        [Header("Patrol Settings")] 
        public Transform[] patrolPoints; // Lista punktów patrolowych
        public float patrolSpeed = 2f; // Prędkość poruszania podczas patrolowania
        private int _currentPatrolIndex = 0; // Aktualny indeks punktu patrolowego

        [Header("Death Settings")] 
        public float deathAnimationTime = 0.5f; // Czas trwania animacji śmierci
        private Animator _animator;
        private bool _isAlive = true; // Czy przeciwnik umiera

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;

        private void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (patrolPoints.Length > 0) {
                GoToNextPatrolPoint();
            }
        }

        private void Update() {
            if (_isAlive) {
                Patrol();
            }
        }

        private void Patrol() {
            if (Vector2.Distance(transform.position, patrolPoints[_currentPatrolIndex].position) < 0.2f) {
                // Jeśli dotarliśmy do punktu patrolowego, przechodzimy do następnego
                _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
                GoToNextPatrolPoint();
            } else {
                // Poruszamy się w kierunku aktualnego punktu patrolowego
                Vector2 direction = (patrolPoints[_currentPatrolIndex].position - transform.position).normalized;
                direction.y = 0;
                _rigidbody2D.velocity = direction * patrolSpeed;
            }
            
            Flip(-_rigidbody2D.velocity.x);
        }
        
        private void Flip(float moveInput) {
            if (moveInput > 0) {
                _spriteRenderer.flipX = false;
            } else if (moveInput < 0) {
                _spriteRenderer.flipX = true;
            }
        }

        private void GoToNextPatrolPoint() {
            if (patrolPoints.Length == 0)
                return;

            // Przeciwnik zmierza do następnego punktu
            Vector2 targetPosition = patrolPoints[_currentPatrolIndex].position;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = direction * patrolSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (!_isAlive)
                return; // Jeśli przeciwnik już umiera, ignorujemy kolizję

            // Sprawdzamy, czy nie gracz wskoczył na głowę przeciwnika
            if (!collision.gameObject.CompareTag("Player"))
                return;
            
            // Sprawdzamy, czy gracz jest nad przeciwnikiem
            float playerHeight = collision.transform.position.y;
            float enemyHeight = transform.position.y;

            if (playerHeight > enemyHeight + 0.5f) { // Dostosuj margines według potrzeby
                // Przeciwnik umiera, zaczynamy animację śmierci
                StartCoroutine(Die());

                // Dodaj odbicie dla gracza po skoku
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null) {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, 10f); // Wartość odbicia
                }
            }
        }

        private IEnumerator Die() {
            _isAlive = false;
            _animator.SetTrigger(DieAnimation); // Aktywuj animację śmierci
            _rigidbody2D.velocity = Vector2.zero; // Zatrzymaj ruch

            yield return new WaitForSeconds(deathAnimationTime); // Czekaj na zakończenie animacji

            Destroy(gameObject); // Usuń przeciwnika
        }
    }
}
