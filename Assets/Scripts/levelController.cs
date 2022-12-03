using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour
{
    public int index;
    public void loadlevel()
    {
        SceneManager.LoadScene(index);
    }
}
