using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PLP.SceneLoading
{
    public class FadeImage : MonoBehaviour
    {
        [SerializeField] private Image _targetImage = null;

        public Sprite TargetSprite = null;

        private Coroutine _currentFading = null;

        public bool IsFinished { get; private set; }

        private IEnumerator FadingDown(Action finished)
        {
            _targetImage.fillAmount = 1;

            while(_targetImage.fillAmount > 0)
            {
                _targetImage.fillAmount -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            finished?.Invoke();
            IsFinished = true;
        }

        private IEnumerator FadingUp(Action finished)
        {
            _targetImage.fillAmount = 0;

            while(_targetImage.fillAmount < 1)
            {
                _targetImage.fillAmount += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            finished?.Invoke();
            IsFinished = true;
        }

        public void Fade(bool isUp = true, Action ended = null)
        {
            if (_currentFading != null) StopCoroutine(_currentFading);

            _targetImage.sprite = TargetSprite;
            IsFinished = false;

            if (isUp == false) _currentFading = StartCoroutine(FadingDown(ended));
            else if (isUp == true) _currentFading = StartCoroutine(FadingUp(ended));
        }
    }
}
