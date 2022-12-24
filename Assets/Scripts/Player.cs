using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Utils;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public UnityEvent OnPlayerDead;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    private Transform _spawnPosition;

    private float dirX = 2f;
    [SerializeField] private float _baseSpeed = 50f;
    [SerializeField] private float _baseJumpForce = 20f;
    [SerializeField] private float _bySizeJumpModifier = 1.16f;
    private float _speed;
    private float _jumpForce;
    [SerializeField] private bool _isSmall = true;
    [SerializeField] private GameObject _smallBallSprite;
    [SerializeField] private GameObject _bigBallSprite;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _popController;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dieSound;
    


    private void Awake()
    {
        _speed = _baseSpeed * 7.2f /*0.12f*/;
        _jumpForce = _baseJumpForce * 19.44f /*2.4f*/;
    }

    void Start()
    {
        
        SetStartedParams();
    }

    void Update()
    {
        
        dirX = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(dirX * _baseSpeed, _rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _baseJumpForce);
            jumpSound.Play();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX > 0f)
        {
            transform.Rotate(0, 0, -360 * 2 * Time.deltaTime);

            _rigidbody.velocity = new Vector2(dirX * _baseSpeed, _rigidbody.velocity.y);
        }
        else if (dirX < 0f)
        {
            transform.Rotate(0, 0, 360 * 2 * Time.deltaTime);
            _rigidbody.velocity = new Vector2(dirX * _baseSpeed, _rigidbody.velocity.y);
        }

    }



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void SetStartedParams()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        _spawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform;
        transform.position = _spawnPosition.position;

        if(_gameManager != null)
        {
            ChangeSize(_gameManager.GetComponent<GameManager>().GetIsStartingSizeSmall(), false);
        }
    }


    public void Respawn()
    {
        StopCoroutine(Respawner());

        _spawnPosition = GameObject.FindGameObjectWithTag("Respawn").transform;

        //ChangeLocalScaleTo(gameObject, 0);
        var tempScale = gameObject.transform.localScale;
        tempScale.x = 0;
        gameObject.transform.localScale = tempScale;

        if (_spawnPosition != null)
        {
            transform.position = _spawnPosition.transform.position;

            if (_gameManager != null)
            {
                var isStartingSizeSmall = _gameManager.GetComponent<GameManager>().GetIsStartingSizeSmall();

                if (isStartingSizeSmall)
                {
                    ChangeSize(true, false);
                }
                else if (isStartingSizeSmall == false)
                {
                    ChangeSize(false, true);
                }


                if (_popController != null)
                {
                    StartCoroutine(Respawner());
                }
            }
        }
    }

    IEnumerator Respawner()
    {
        yield return new WaitForSecondsRealtime(_popController.GetComponent<PopController>()._popShowTime);

        //ChangeLocalScaleTo(gameObject, 1);


        var tempScale = gameObject.transform.localScale;
        tempScale.x = 1;
        gameObject.transform.localScale = tempScale;

        StopCoroutine(Respawner());
    }

    public void Died()
    {
        //ToDo: сделать нормальную механику;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSize(bool toSmall, bool changeJumpForce = false)
    {
        // ToDo: оптимизировать через класс объекта?
        if (toSmall)
        {
            _bigBallSprite.SetActive(false);
            _smallBallSprite.SetActive(true);
            _isSmall = true; // Не выноси это за условный блок.
        }
        else
        {
            _smallBallSprite.SetActive(false);
            _bigBallSprite.SetActive(true);
            _isSmall = false; // Аналогично.
        }

        if (toSmall && changeJumpForce)
        {
            _jumpForce /= _bySizeJumpModifier;
            return;
        }
        else if(!toSmall && changeJumpForce)
        {
            _jumpForce *= _bySizeJumpModifier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Life")
        {
            LifeCountManager.ChangeCountTo(1);
            Destroy(collision.gameObject);
        }

        if(collision.transform.tag == "Portal")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.transform.tag == "Thorn")
        {
            dieSound.Play();

            if (LifeCountManager.GetCount() > 1)
            {
                LifeCountManager.ChangeCountTo(-1);
                OnPlayerDead?.Invoke();
                Respawn();
                return;
            }
            else
            {
                OnPlayerDead?.Invoke();
                Died();
                return;
            }
        }

    }
  
}
