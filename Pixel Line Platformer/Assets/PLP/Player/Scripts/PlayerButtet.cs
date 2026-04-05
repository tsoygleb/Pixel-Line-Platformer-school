using UnityEngine;

namespace PLP
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 4.5f;
        [SerializeField] private float _lifeTime = 4f;

        private void Update()
        {
            _lifeTime -= 1 * Time.deltaTime;

            if (_lifeTime <= 0) Destroy(gameObject);

            transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }   
}
