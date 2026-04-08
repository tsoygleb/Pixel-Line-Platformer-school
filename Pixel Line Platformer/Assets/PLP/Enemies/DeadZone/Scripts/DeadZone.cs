using PLP.Player;
using UnityEngine;

namespace PLP.Enemies.DeadZone
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerHealth component) == true)
            {
                component.TakeDamage();
            }
        }
    }   
}
