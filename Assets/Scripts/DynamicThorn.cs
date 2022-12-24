using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicThorn : MonoBehaviour
{
    [SerializeField] private bool needVerticalMove = true;
    [SerializeField] private bool needHorizontalMove = false;
    [SerializeField] private float _minYOffset = 0;
    [SerializeField] private float _maxYOffset = 0;
    [SerializeField] private float _minXOffset = 0;
    [SerializeField] private float _maxXOffset = 0;
    private bool toTop = false;
    private bool toRight = false;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        CheckPosition();
        var position = transform.position;
        var offset = 1.5f * Time.deltaTime;

        if (needHorizontalMove)
        {
            position.x += ((toRight) ? offset : -offset);
        }
        if (needVerticalMove)
        {
            position.y += ((toTop) ? offset : -offset);
        }

        transform.position = position;
    }

    private void CheckPosition()
    {
        var position = transform.position;

        if (needHorizontalMove)
        {
            if (position.x <= _minXOffset)
            {
                toRight = true;
            }
            else if (position.x >= _maxXOffset)
            {
                toRight = false;
            }
        }

        if (needVerticalMove)
        {
            if (position.y <= _minYOffset)
            {
                toTop = true;
            }
            else if (position.y >= _maxYOffset)
            {
                toTop = false;
            }
        }
    }
}
