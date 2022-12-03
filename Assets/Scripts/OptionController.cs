using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    public float _Volume = 1f;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOption();
    }

    void Update()
    {

    }

    public void loadOption()
    {
        _Volume = PlayerPrefs.GetFloat(key: "volume", defaultValue: 1f);
    }

    public void saveOption()
    {
        PlayerPrefs.SetFloat("volume", _Volume);
    }
}
