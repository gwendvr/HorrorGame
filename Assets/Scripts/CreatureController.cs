using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{

    public PlayerController _player;
    public Animator _Animator;
    public AudioSource _ISeeYou;
    public GameObject _Creature;
    public Vector2 positionMaxLeft, positionMaxRight;
    public bool _wanderOnLeft, _wanderOnRight;


    [SerializeField] bool _isTriggered = false;
    private float _sideForBegginWandering = 0f;
    private SpriteRenderer _sprite;
    [SerializeField] bool _isWandering, areaReseted = false;
    private float _SpeedWhenTriggered = 6f, _SpeedWhenNotTriggered = 4f, _speed;
    private Rigidbody2D rb;
    private float _isBlocked = 1f;

    void Start()
    {
        _Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        _sprite = GetComponent<SpriteRenderer>();
        _ISeeYou = GetComponent<AudioSource>();
        _ISeeYou.volume = _ISeeYou.volume * PlayerPrefs.GetFloat("volume");

        if (positionMaxLeft.x == 0)
        {
            positionMaxLeft.x = rb.position.x + 5f;
        }
        if (positionMaxRight.x == 0)
        {
            positionMaxRight.x = rb.position.x - 5f;
        }



        StartCoroutine(Wander());
    }


    private void FixedUpdate()
    {
        if (!_isWandering && !_isTriggered)
        {
            StartCoroutine(Wander());
        }
        rb.MovePosition(rb.position + Vector2.right * _speed * Time.deltaTime * _isBlocked);
    }

    public void OnTriggerEnter2D(Collider2D area)
    {

        if (area.CompareTag("Player"))
        {
            _isTriggered = true;
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
            _wanderOnLeft = false;
            _wanderOnRight = false;
        }

        if (area.CompareTag("WallCreature"))
        {

            _sprite.color = Color.black;
            _isBlocked = 0f;
        }

    }
    public void OnTriggerExit2D(Collider2D area)
    {
        if (area.CompareTag("Player"))
        {
            _isTriggered = false;
            _isWandering = false;
            _speed = 0f;
            _Animator.SetBool("Walk", false);
            _ISeeYou.Stop();
            StartCoroutine(NewWanderingArea());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if (_player.HitACreature())
            {
                Destroy(_Creature);
            }
        }

    }


    public IEnumerator NewWanderingArea()
    {
        
        positionMaxLeft.x = rb.position.x + 15f;
        positionMaxRight.x = rb.position.x - 15f;
        areaReseted = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Wander());
    } 

    public IEnumerator Wander()
    {
        if (!_isTriggered)
        {
            //attendre avant de reprendre le wandering
            if (areaReseted)
            {
                yield return new WaitForSeconds(4f);
                areaReseted = false;
            }
            _isWandering = true;
            //determine dans quel sens va la créature
            _sideForBegginWandering = Random.Range(-1f, 1f);
            rb.MovePosition(rb.position + Vector2.left * _sideForBegginWandering);
            if (!_wanderOnLeft && !_wanderOnRight)
            {
                if (_sideForBegginWandering < 0)
                {
                    _wanderOnLeft = true;
                }
                else
                {
                    _wanderOnRight = true;
                }
            }

            if (_wanderOnLeft)
            {
                if (rb.position.x <= positionMaxLeft.x)
                {
                    _speed = _SpeedWhenNotTriggered;
                    _sprite.flipX = true;
                    _Animator.SetBool("Walk", true);
                }
                else
                {
                    _speed = 0f;
                    _Animator.SetBool("Walk", false);
                    yield return new WaitForSeconds(3f);
                    _wanderOnLeft = false;
                    _wanderOnRight = true;
                }
            }
            else if (_wanderOnRight)
            {
                if (rb.position.x >= positionMaxRight.x)
                {
                    _Animator.SetBool("Walk", true);
                    _speed = -_SpeedWhenNotTriggered;
                    _sprite.flipX = false;
                }
                else
                {
                    _speed = 0f;
                    _Animator.SetBool("Walk", false);
                    yield return new WaitForSeconds(3f);
                    _wanderOnRight = false;
                    _wanderOnLeft = true;

                }
            }
            _isWandering = false;

        }
    }
}
