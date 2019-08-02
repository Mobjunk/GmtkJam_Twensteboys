using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace r0w3ntje
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        private void Start()
        {
            ToScene(1);
        }

        public void ToScene(int _sceneIndex)
        {
            StartCoroutine(LoadScene(_sceneIndex));
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f) break;
                yield return null;
            }

            FadeManager.Instance().FadeWithAction(() => { asyncOperation.allowSceneActivation = true; });
        }
    }
}