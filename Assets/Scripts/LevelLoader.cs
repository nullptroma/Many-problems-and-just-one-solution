using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Blackout bl;
    public string nextSceneName;
    private bool _reloading;

    public void ReloadScene()
    {
        if(_reloading || bl.Started)
            return;
        _reloading = true;
        bl.Black(()=>SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
