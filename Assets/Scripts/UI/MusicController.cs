using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Sprite enable;
    [SerializeField] private Sprite disable;
    
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            Music.Enable = !Music.Enable;
            UpdateImg();
        });            
        UpdateImg();
    }

    void UpdateImg()
    {
        btn.image.sprite = Music.Enable ? enable : disable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
