using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    public PlayerController _player;


    private bool _IsTrigered = false;
    [SerializeField] float _Speed = 6f;
    [SerializeField] float _LeftOrRight = 0;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_IsTrigered)
        {
            if (this.transform.position.x - _player.transform.position.x < 0) rb.MovePosition(rb.position + Vector2.right * _Speed * Time.deltaTime);
            else rb.MovePosition(rb.position + Vector2.left * _Speed * Time.deltaTime);
        }
        else rb.MovePosition(rb.position);
    }

    public void OnTriggerEnter2D(Collider2D area)
    {
        if (area.CompareTag("Player")) _IsTrigered = true;

    }
    public void OnTriggerExit2D(Collider2D area)
    {
        if (area.CompareTag("Player")) _IsTrigered = false;
    }


}
