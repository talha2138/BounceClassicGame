using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject _checkPointActivate;
    [SerializeField] private Transform _checkPoints;
    private Transform _spawnPisition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _spawnPisition = GameObject.FindGameObjectWithTag("Respawn").transform;

            if (_checkPointActivate != null && _checkPoints != null && _spawnPisition != null)
            {
                _spawnPisition.position = gameObject.transform.position;

                var newPoint = Instantiate(_checkPointActivate, _checkPoints);
                newPoint.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}
