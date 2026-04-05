using System.Collections;
using UnityEngine;

namespace PLP.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPoint = null;
        [SerializeField] private PlayerBullet _bulletPrefab = null;
        [SerializeField] private float _shootCooldown = 0.4f;
        [Space(6)]
        [SerializeField] private Animator _animator = null;

        private bool _isCooldown = false;
        private bool _isWeaponActive = false;

        private Coroutine _animationResetintCoroutine = null;

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_shootCooldown);
            _isCooldown = false;
        }

        private IEnumerator AnimationReseting()
        {
            yield return new WaitForSeconds(0.01f);
            _animator.ResetTrigger("Shoot");
        }

        public void ActiveWeapon()
        {
            if (_isWeaponActive == true) return;

            _isWeaponActive = true;
            _animator.SetBool("IsWeapon", true);
            _animator.SetTrigger("TakeWeapon");
        }

        public void Shoot()
        {
            if (_isWeaponActive == false) return;
            if (_isCooldown == true) return;

            _isCooldown = true;
            _animator.SetTrigger("Shoot");
            if (_animationResetintCoroutine != null) StopCoroutine(_animationResetintCoroutine);

            StartCoroutine(Cooldown());
            _animationResetintCoroutine = StartCoroutine(AnimationReseting());

            Instantiate(_bulletPrefab, _bulletSpawnPoint.transform.position, transform.rotation);
        }
    }   
}
