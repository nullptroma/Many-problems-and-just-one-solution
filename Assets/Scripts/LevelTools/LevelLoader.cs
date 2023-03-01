using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Blackout _bl;
    public string nextSceneName;
    private bool _reloading;

    private void Start()
    {
        _bl = FindObjectOfType<Blackout>();
    }

    public void ReloadScene()
    {
        if(_reloading || (_bl!=null && _bl.Started))
            return;
        _reloading = true;
        if (_bl != null)
            _bl.Black(() => Load(SceneManager.GetActiveScene().name));
        else
            Load(SceneManager.GetActiveScene().name);
    }
    
    public void NextScene()
    {
        if (_bl != null)
            _bl.Black(() => Load(nextSceneName));
        else
            Load(nextSceneName);
    }

    public void LoadMenu()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Menu"))
            _bl.Black(() => Load("Menu"));
    }

    private void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
