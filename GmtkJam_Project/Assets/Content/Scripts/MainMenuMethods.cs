using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMethods : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] GameObject one;
    [SerializeField]  GameObject two;

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void SwitchPanels()
    {
        if (one.activeSelf)
        {
            one.SetActive(false);
            two.SetActive(true);
        }
        else
        {
            one.SetActive(true);
            two.SetActive(false);
        }
    }
}
