using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    public PlayerController _player;
    public Animator _Animator;
    public AudioSource _ISeeYou;



    private bool _wanderOnLeft, _wanderOnRight;
    private float _sideForBegginWandering = 0f;
    [SerializeField] Vector2 positionMaxLeft, positionMaxRight;
    private SpriteRenderer _sprite;
    [SerializeField] float _speed;
    private bool _isWandering;
    [SerializeField] float _SpeedWhenTriggered = 6f;
    [SerializeField] float _SpeedWhenNotTriggered = 4f;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        _Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        _sprite = GetComponent<SpriteRenderer>();
        _ISeeYou = GetComponent<AudioSource>();
        positionMaxLeft.x = rb.position.x + 5f;
        positionMaxRight.x = rb.position.x - 5f;

        StartCoroutine(Wander());
    }


    private void FixedUpdate()
    {
        if (!_isWandering)
        {
            StartCoroutine(Wander());
        }
        rb.MovePosition(rb.position + Vector2.right * _speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D area)
    {

        if (area.CompareTag("Player"))
        {
            StopCoroutine(Wander());
            
            if (this.transform.position.x - _player.transform.position.x < 0)
            {
                _speed = _SpeedWhenTriggered;
                _sprite.flipX = true;
            }
            else
            {
                _speed = -_SpeedWhenTriggered;
                _sprite.flipX = false;
            }
            _Animator.SetBool("Walk", true);
            _ISeeYou.Play();
        }
    }
    public void OnTriggerExit2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            _speed = 0f;
            _Animator.SetBool("Walk", false);
            _ISeeYou.Stop();
            StartCoroutine(NewWanderingArea());
        }
    }

    public IEnumerator NewWanderingArea()
    {
        positionMaxLeft.x = rb.position.x - 5f;
        positionMaxRight.x = rb.position.x + 5f;
        yield return new WaitForSeconds(4f);
        StartCoroutine(Wander());
    }

    public IEnumerator Wander()
    {
        _isWandering = true;
        //determine dans quel sens va la créature
        _sideForBegginWandering = Random.Range(-1f, 1f);
        rb.MovePosition(rb.position + Vector2.left * _sideForBegginWandering);
        if (_sideForBegginWandering < 0)
        {
            _wanderOnLeft = true;
        }
        else
        {
            _wanderOnRight = true;
        }

        if (_wanderOnLeft)
        {
            if (rb.position.x < positionMaxLeft.x)
            {
                _speed = _SpeedWhenNotTriggered;
                _sprite.flipX = false;
            }
            else
            {
                _speed = 0f;
                yield return new WaitForSeconds(3f);
                _wanderOnLeft = false;
            }
        }
        else if (_wanderOnRight)
        {
            if (rb.position.x > positionMaxRight.x)
            {
                _speed = -_SpeedWhenNotTriggered;
                _sprite.flipX = true;
            }
            else
            {
                _speed = 0f;
                yield return new WaitForSeconds(3f);
                _wanderOnRight = false;
            }
        }
        _isWandering = false;
    }
}
