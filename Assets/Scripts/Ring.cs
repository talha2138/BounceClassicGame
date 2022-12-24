using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Ring : MonoBehaviour
{
    public UnityEvent OnRingEnter;

    [SerializeField] private GameObject _inactiveRing;
    [SerializeField] private Transform _rings;
    [SerializeField] private bool _needRotate = false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(_inactiveRing != null && _rings != null)
            {
                var newRing = Instantiate(_inactiveRing, _rings);
                newRing.transform.position = transform.position;
                
                if (_needRotate)
                {
                    newRing.transform.rotation = Quaternion.Euler(0, 0, 90);
                }

                Destroy(gameObject);

                OnRingEnter?.Invoke();
            }
        }
    }
}
