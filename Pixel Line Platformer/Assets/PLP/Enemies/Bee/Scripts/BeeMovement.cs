using UnityEngine;

namespace PLP.Enemies.Bee
{
    public class BeeMovement : MonoBehaviour
    {
        [SerializeField] private Transform _beeTransform = null;
        [SerializeField] private Transform[] _points = new Transform[0];
        [SerializeField] private float _movingSpeed = 4f;

        private int _currentPointIndex = 0;

        private void Start()
        {
            if (_points.Length == 0) return;

            int index = 0;
            float lastDistance = Vector2.Distance(transform.position, _points[0].position);

            for (int i = 0; i < _points.Length; i++)
            {
                if (Vector2.Distance(_beeTransform.position, _points[i].position) < lastDistance)
                {
                    lastDistance = Vector2.Distance(_beeTransform.position, _points[0].position);
                    index = i;
                }
            }

            _currentPointIndex = index;
        }

        private void Update()
        {
            if (_points.Length == 0) return;

            if (Vector2.Distance(_beeTransform.position, _points[_currentPointIndex].position) <= 0)
            {
                if (_currentPointIndex == _points.Length - 1) _currentPointIndex = 0;
                else _currentPointIndex++;

                Flip();
            }

            _beeTransform.position = Vector2.MoveTowards(_beeTransform.position, _points[_currentPointIndex].position, _movingSpeed * Time.deltaTime);
        }

        private void Flip()
        {
            Vector2 direction = ((Vector2)_points[_currentPointIndex].position - (Vector2)_beeTransform.position).normalized;

            if (direction.x < 0) _beeTransform.rotation = Quaternion.Euler(0, 180, 0);
            else if (direction.x > 0) _beeTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }   
}
