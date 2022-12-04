using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSoundController : MonoBehaviour
{
    AudioSource _Rain;
    // Start is called before the first frame update
    void Start()
    {
        _Rain= GetComponent<AudioSource>();
        _Rain.volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
