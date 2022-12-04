using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public OptionController _oc;
    public GameObject selectOption;
    public GameObject selectSound;
    public GameObject selectDoor;
    public GameObject menu;
    public GameObject option;
    public GameObject menuBg;
    public GameObject levelBg;

    AudioSource _MainMusic;
    void Start()
    {
        _oc = FindObjectOfType<OptionController>();
        menu.SetActive(true);
        option.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectOption);
        _MainMusic = GetComponent<AudioSource>();
        _MainMusic.volume = PlayerPrefs.GetFloat("volume");

    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void setting()
    {
        menu.SetActive(false);
        option.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectSound);
    }

    public void saveOption()
    {
        _oc.saveOption();
        menu.SetActive(true);
        option.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectOption);
        _MainMusic.volume = PlayerPrefs.GetFloat("volume");
    }

    public void showLevels()
    {
        menuBg.SetActive(false);
        levelBg.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectDoor);
    }
}
