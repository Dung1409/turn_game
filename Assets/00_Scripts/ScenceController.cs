using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScenceController : Singleton<ScenceController> 
{
    [SerializeField] Slider Loading;

    private void Update()
    {
        StartCoroutine(LoadingLevelAsync(1));
    }

    IEnumerator LoadingLevelAsync(int level)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(level);
        while (!load.isDone)
        {
            
            float process = Mathf.Clamp01((float)load.progress / 0.9f);
            Loading.value = process;
            yield return null;
        }
    }
    
}
