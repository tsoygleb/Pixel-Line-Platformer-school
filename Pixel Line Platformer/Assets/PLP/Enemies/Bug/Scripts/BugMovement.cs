using PLP.Player;
using UnityEngine;

namespace PLP.Enemies.Bug
{
    public class BugMovement : MonoBehaviour
    {
        [SerializeField] private Transform _bugTransform = null;
        [Space(6)]
        [SerializeField] private float _movingDistance = 4f;
        [SerializeField] private float _movingSpeed = 1f;
        [Space(6)]
        [SerializeField] private LayerMask _groundLayer = 1<<6;

        private Transform _playerTransform = null;

        private void Start()
        {
            _playerTransform = FindAnyObjectByType<PlayerController>().transform;
        }

        private void OnDrawGizmos()
        {
            if (_bugTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_bugTransform.position, _movingDistance);
            }
        }

        private void Update()
        {
            if (_playerTransform == null) return;

            if (Vector2.Distance(_bugTransform.position, _playerTransform.transform.position) <= _movingDistance)
            {
                Vector2 direction = ((Vector2)_playerTransform.transform.position - (Vector2)_bugTransform.position).normalized;

                if (Physics2D.Raycast(_bugTransform.position, direction, _movingDistance, _groundLayer) == false)
                {
                    _bugTransform.position = Vector2.MoveTowards(_bugTransform.position, _playerTransform.transform.position, _movingSpeed * Time.deltaTime);

                    Flip();
                }
            }
        }

        private void Flip()
        {
            Vector2 direction = ((Vector2)_playerTransform.transform.position - (Vector2)_bugTransform.position).normalized;

            if (direction.x < 0) _bugTransform.rotation = Quaternion.Euler(0, 180, 0);
            else if (direction.x > 0) _bugTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }   
}
