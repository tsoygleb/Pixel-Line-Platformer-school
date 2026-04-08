using UnityEngine;

namespace PLP.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public void TakeDamage()
        {
            gameObject.SetActive(false);
        }
    }   
}
