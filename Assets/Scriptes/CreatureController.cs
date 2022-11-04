using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    public PlayerController _player;
    public Animator _Animator;
    public AudioSource _ISeeYou;

    private SpriteRenderer _sprite;
    private bool _IsTrigered = false;
    [SerializeField] float _Speed = 6f;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        _sprite = GetComponent<SpriteRenderer>();
        _ISeeYou = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_IsTrigered)
        {
            if (this.transform.position.x - _player.transform.position.x < 0)
            {
                rb.MovePosition(rb.position + Vector2.right * _Speed * Time.deltaTime);
                _sprite.flipX = true;
            }
            else
            {
                rb.MovePosition(rb.position + Vector2.left * _Speed * Time.deltaTime);
                _sprite.flipX = false;
            }
        }
        else
        {
            rb.MovePosition(rb.position);
        }
    }

    public void OnTriggerEnter2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            _IsTrigered = true;
            _Animator.SetBool("Walk", true);
            _ISeeYou.Play();
        }
    }
    public void OnTriggerExit2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            _IsTrigered = false;
            _Animator.SetBool("Walk", false);
            _ISeeYou.Stop();

        }
    }


}
