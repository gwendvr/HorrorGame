using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject _Backgroud;
    public GameObject _Play;
    public GameObject _Parameters;
    public GameObject _Quit;
    public GameObject _Pause;
    public GameObject _ParametersMenu;
    public OptionController _oc;
    public GameObject selectOption;




    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(selectOption);
        _Pause.SetActive(false);
        _Backgroud.SetActive(false);
        _Play.SetActive(false);
        _Parameters.SetActive(false);
        _Quit.SetActive(false);
        _ParametersMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainPauseMenu()
    {
        _Backgroud.SetActive(true);
        _Play.SetActive(true);
        _Parameters.SetActive(true);
        _Quit.SetActive(true);
        _Pause.SetActive(true);
    }
    public void Play()
    {
        _Pause.SetActive(false);
        _Backgroud.SetActive(false);
        _Play.SetActive(false);
        _Parameters.SetActive(false);
        _Quit.SetActive(false);
        _ParametersMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public void Parameters()
    {
        _Backgroud.SetActive(false);
        _Play.SetActive(false);
        _Parameters.SetActive(false);
        _Quit.SetActive(false);
        _ParametersMenu.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveOption()
    {
        _oc.saveOption();
        _Play.SetActive(true);
        _Parameters.SetActive(true);
        _Quit.SetActive(true);
        _ParametersMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectOption);
    }

}
