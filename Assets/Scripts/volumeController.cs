using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeController : MonoBehaviour
{
    public OptionController _oc;
    public Slider _slider;
    void Start()
    {
        _oc = FindObjectOfType<OptionController>();
        _slider = GetComponent<Slider>();
        _slider.value = _oc._Volume;
    }


    void Update()
    {

    }

    public void onValueChange(float value)
    {
        _oc._Volume = value;
    }

}
