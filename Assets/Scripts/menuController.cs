using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject selectOption;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(selectOption);
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

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
