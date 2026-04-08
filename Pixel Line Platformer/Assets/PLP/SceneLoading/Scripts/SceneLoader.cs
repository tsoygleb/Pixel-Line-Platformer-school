using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PLP.SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Sprite _fadeSprite = null;
        [SerializeField] private FadeImage _targetFadeImage = null;
        [Header("Canvas:")]
        [SerializeField] private Canvas _targetCanvas = null;
        [SerializeField, Min(1)] private int _resolutionX = 0;
        [SerializeField, Min(1)] private int _resolutionY = 0;
        [Header("Others:")]
        [SerializeField] private UnityEngine.Camera _targetCamera = null;
        [SerializeField] private bool _findCameraOnStart = true;

        private bool _isLoading = false;

        private CanvasScaler _canvasScaler = null;

        private static SceneLoader instance;
        public static SceneLoader Instance { get { return instance; } private set { } }

        private void OnValidate()
        {
            if (_targetCamera != null) _targetCanvas.worldCamera = _targetCamera;

            if (_targetCanvas != null)
            {
                if (_canvasScaler == null || _canvasScaler != _targetCanvas.GetComponent<CanvasScaler>()) _canvasScaler = _targetCanvas.GetComponent<CanvasScaler>();

                _canvasScaler.referenceResolution = new Vector2(_resolutionX, _resolutionY);
            }

            if (_fadeSprite != null) _targetFadeImage.TargetSprite = _fadeSprite;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _targetCamera = UnityEngine.Camera.main;
        }

        private IEnumerator SceneLoading(int sceneIndex)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            asyncOperation.allowSceneActivation = false;

            yield return new WaitForSeconds(0.3f);

            _targetFadeImage.Fade(true, delegate { });

            while(_targetFadeImage.IsFinished == false)
            {
                yield return null;
            }

            asyncOperation.allowSceneActivation = true;

            yield return new WaitForSeconds(0.3f);

            _targetFadeImage.Fade(false, delegate { _targetFadeImage.gameObject.SetActive(false); });

            _isLoading = false;
        }

        public void LoadScene(int sceneIndex)
        {
            if (_isLoading == true)
            {
                Debug.Log("Scene is loading now");
                return;
            }

            _isLoading = true;
            _targetFadeImage.gameObject.SetActive(true);

            StartCoroutine(SceneLoading(sceneIndex));
        }

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }   
}
