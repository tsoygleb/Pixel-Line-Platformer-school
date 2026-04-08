using UnityEngine;

namespace PLP.Enemies.Worm
{
    public class WormMovement : MonoBehaviour
    {
        [SerializeField] private Transform _wormTransform = null;
        [Space(6)]
        [SerializeField] private Transform _firstPoint = null;
        [SerializeField] private Transform _secondPoint = null;
        [Space(6)]
        [SerializeField] private float _movingSpeed = 4f;

        private Vector2 _targetPosition = Vector2.zero;

        private void Start()
        {
            if (Vector2.Distance(_wormTransform.position, _firstPoint.position) < Vector2.Distance(_wormTransform.position, _secondPoint.position))
            {
                _targetPosition = new Vector2(_firstPoint.position.x, _wormTransform.position.y);
            }
            else
            {
                _targetPosition = new Vector2(_secondPoint.position.x, _wormTransform.position.y);
            }
        }

        private void Update()
        {
            if (_wormTransform.position.x == _firstPoint.position.x) _targetPosition = new Vector2(_secondPoint.position.x, _wormTransform.position.y);
            else if (_wormTransform.position.x == _secondPoint.position.x) _targetPosition = new Vector2(_firstPoint.position.x, _wormTransform.position.y);

            Flip();

            _wormTransform.position = Vector2.MoveTowards(_wormTransform.position, _targetPosition, _movingSpeed * Time.deltaTime);
        }

        private void Flip()
        {
            Vector2 direction = (_targetPosition - (Vector2)_wormTransform.position).normalized;

            if (direction.x < 0) _wormTransform.rotation = Quaternion.Euler(0, 180, 0);
            else if (direction.x > 0) _wormTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }   
}
