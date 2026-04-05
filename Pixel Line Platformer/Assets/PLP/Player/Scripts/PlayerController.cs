using UnityEngine;

namespace PLP.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movementComponenet = null;
        [SerializeField] private PlayerShooter _shootingComponent = null;

        private void Update()
        {
            Vector2 inputs = Vector2.zero;
            inputs.x = Input.GetAxisRaw("Horizontal");

            _movementComponenet.Move(inputs.x);

            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                _movementComponenet.Jump();
            }

            if (Input.GetKeyDown(KeyCode.Alpha8) == true || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.UpArrow) == true)
            {
                _shootingComponent.Shoot();
            }
        }
    }   
}
