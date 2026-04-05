using UnityEngine;

namespace PLP.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movingSpeed = 4f;
        [SerializeField] private float _jumpForce = 4f;
        [Header("Ground trigger:")]
        [SerializeField] private Transform _groundTriggerTransfrom = null;
        [SerializeField, Min(0)] private float _groundCheckRadius = 0.3f;
        [SerializeField] private LayerMask _groundLayer = 1<<6;
        [Header("Others:")]
        [SerializeField] private Animator _animator = null;

        private Rigidbody2D _rigidbody2D = null;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _animator.SetBool("IsGround", CheckGround());
        }

        private void OnDrawGizmos()
        {
            if (_groundTriggerTransfrom != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_groundTriggerTransfrom.position, _groundCheckRadius);
            }
        }

        private bool CheckGround()
        {
            return Physics2D.OverlapCircle(_groundTriggerTransfrom.position, _groundCheckRadius, _groundLayer);
        }

        private void Flip(float directionX)
        {
            Quaternion newRotation = transform.rotation;

            if (transform.rotation.y == 0 && directionX < 0) newRotation = Quaternion.Euler(0, 180, 0);
            else if (transform.rotation.y == -1 && directionX > 0) newRotation = Quaternion.Euler(0, 0, 0);

            transform.rotation = newRotation;
        }

        public void Move(float directionX)
        {
            Vector2 newVelocity = _rigidbody2D.linearVelocity;
            newVelocity.x = _movingSpeed * directionX;
            _rigidbody2D.linearVelocity = newVelocity;

            if (directionX != 0) _animator.SetBool("IsRunning", true);
            else _animator.SetBool("IsRunning", false);

            Flip(directionX);
        }

        public void Jump()
        {
            if (CheckGround() == false) return;

            _rigidbody2D.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }   
}
