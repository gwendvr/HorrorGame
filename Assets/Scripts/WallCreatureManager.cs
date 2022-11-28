using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WallCreatureManager : MonoBehaviour
{

    public Rigidbody2D rb;
    public int _stage = 0;
    public float _speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + Vector2.right * _speed * Time.deltaTime);
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
            case 2: _speed = 3f;
                break;
            case 3: _speed = 15f;
                break;
        }
    }
}
