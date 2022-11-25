using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{

    public GameObject _Knife;
    // Start is called before the first frame update
    void Start()
    {
        _Knife.SetActive(false);
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
}
