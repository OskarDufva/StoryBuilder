using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingPage : MonoBehaviour
{
   public void LoadScene(sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}