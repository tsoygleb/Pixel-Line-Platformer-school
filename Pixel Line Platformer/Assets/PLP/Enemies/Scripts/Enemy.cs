using PLP.Player;
using UnityEngine;

namespace PLP.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public void TakeDamage()
        {
            gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerHealth component) == true)
            {
                component.TakeDamage();
            }
        }
    }   
}
