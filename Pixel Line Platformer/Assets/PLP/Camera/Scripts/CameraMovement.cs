using UnityEngine;

namespace PLP.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform = null;
        [SerializeField] private float _movingSpeed = 2.5f;

        private void LateUpdate()
        {
            if (_targetTransform != null)
            {
                Vector3 targetPosition = _targetTransform.position;
                targetPosition.z = -10;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movingSpeed * Time.fixedDeltaTime);
            }
        }
    }   
}
