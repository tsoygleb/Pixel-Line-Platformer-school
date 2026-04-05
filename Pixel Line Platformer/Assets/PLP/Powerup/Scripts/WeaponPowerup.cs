using PLP.Player;
using UnityEngine;

namespace PLP.Powerup
{
    public class WeaponPowerup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerShooter playerShooter) == true)
            {
                playerShooter.ActiveWeapon();
                Destroy(gameObject);
            }
        }
    }   
}
