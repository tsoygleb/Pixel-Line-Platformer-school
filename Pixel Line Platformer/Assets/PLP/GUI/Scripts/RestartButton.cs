using PLP.SceneLoading;
using UnityEngine;

namespace PLP.GUI
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            SceneLoader.Instance.ReloadScene();
        }
    }   
}
