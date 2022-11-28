using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidacticielController : MonoBehaviour
{
    PlayerController _Player;
    
    // Start is called before the first frame update
    void Start()
    {
        _Player = GetComponent<PlayerController>();
        _Player.IsOnDidacticiel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
