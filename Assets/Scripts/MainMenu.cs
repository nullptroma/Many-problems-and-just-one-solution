using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button nextLvlButton;
    [SerializeField] private Button prevLvlButton;
    [SerializeField] private TextMeshProUGUI currLvlText;
    [SerializeField] private int maxLvl;
    private int _curLvl;
    
    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(()=>Application.Quit());
        nextLvlButton.onClick.AddListener(() =>
        {
            _curLvl++;
            currLvlText.text = _curLvl.ToString();
            prevLvlButton.interactable = true;
            if (_curLvl >= maxLvl)
                nextLvlButton.interactable = false;
        });
        prevLvlButton.onClick.AddListener(() =>
        {
            _curLvl--;
            currLvlText.text = _curLvl.ToString();
            nextLvlButton.interactable = true;
            if (_curLvl <= 1)
                prevLvlButton.interactable = false;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
