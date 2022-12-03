using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{

    public GameObject _GetCrazyStep1, _GetCrazyStep2, _GetCrazyStep3;
    public Animator _AnimCrazyStep1, _AnimCrazyStep2, _AnimCrazyStep3;
    public GameObject _Knife;
    // Start is called before the first frame update
    void Start()
    {
        _Knife.SetActive(false);
        _AnimCrazyStep1.SetBool("Crazy1", true);
        _AnimCrazyStep2.SetBool("Crazy2", true);
        _AnimCrazyStep3.SetBool("Crazy3", false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowKnifeInventory()
    {
        _Knife.SetActive(true);
    }

    public void HideKnifeInventory()
    {
        _Knife.SetActive(false);
    }

    //Crazy Effects
    public void ShowCrazy1()
    {
        _AnimCrazyStep1.SetBool("Crazy1", true);
    }
    public void HideCrazy1()
    {
        _AnimCrazyStep1.SetBool("Crazy1", false);
    }


    public void ShowCrazy2()
    {
        _AnimCrazyStep2.SetBool("Crazy2", true);
    }
    public void HideCrazy2()
    {
        _AnimCrazyStep2.SetBool("Crazy2", false);
    }


    public void ShowCrazy3()
    {
        _AnimCrazyStep3.SetBool("Crazy3", true);
    }
    public void HideCrazy3()
    {
        _AnimCrazyStep3.SetBool("Crazy3", false);
    }
}