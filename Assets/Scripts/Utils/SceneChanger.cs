using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneChanger: MonoBehaviour
    {
        public void ChangeScene(int index)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(index);
            asyncOperation.completed += (operation) => AudioManager.Instance.PoolAudioSources();
        }
    }
}