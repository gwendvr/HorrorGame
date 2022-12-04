using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WallCreatureManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameObject _Player;
    public int _stage = 0;
    public float _speed = 5f;
    private AudioSource _Audio;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _Audio= GetComponent<AudioSource>();
        _Audio.volume = 0.6f * PlayerPrefs.GetFloat("volume");

    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + Vector2.right * _speed * Time.deltaTime);
        //_Audio.volume = (_Player.transform.position.x / rb.position.x) - 0.5f;
    }

    private void FixedUpdate()
    {
        if (_Audio.volume != 0.6f * PlayerPrefs.GetFloat("volume"))
        {
            _Audio.volume = 0.6f * PlayerPrefs.GetFloat("volume");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedChange"))
        {
            _stage += 1;
        }
        switch (_stage)
        {
            case 0: _speed = 7.5f;
                break;
            case 1: _speed = 1.5f;
                break;
            case 2: _speed = 2f;
                break;
            case 3: _speed = 5f;
                break;
        }
    }

}
