using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopController : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject _pop;
    internal float _popShowTime = 1f;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.Translate(0, 100f, 0);
    }

    public void ShowPop()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        if (_player != null)
        {
            StartCoroutine(PopShower());
        }
    }

    IEnumerator PopShower()
    {
        Time.timeScale = 0;
        if (_player == null)
        {
            yield return new WaitForSeconds(_popShowTime);
            StopCoroutine(PopShower());
        }

        if(_pop != null)
        {
            _pop.transform.position = _player.transform.position;
            yield return new WaitForSecondsRealtime(_popShowTime);
            Time.timeScale = 1;
            _pop.transform.Translate(0, 100f, 0);
        }
    }
}
