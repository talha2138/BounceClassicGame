using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnRingCountEnd;

    private GameObject _player;
    [SerializeField] internal bool isStartingSizeSmall = true;
    private int _ringsCount;
    [SerializeField] private Transform _ringCounter;
    [SerializeField] private GameObject _ringImage;
    [SerializeField] private float _ringWidth = 0.35f;
    [SerializeField] private AudioSource jumpSound;
    private List<GameObject> _ringImages = new List<GameObject>();

    private void Start()
    {
        StopAllCoroutines();
        Time.timeScale = 1;

        _player = GameObject.FindGameObjectWithTag("Player");

        CreateRingImages();
    }

    private void Update()
    {
        if(_ringsCount <= 0)
        {
            OnRingCountEnd?.Invoke();
        }
    }

    public bool GetIsStartingSizeSmall()
    {
        return isStartingSizeSmall;
    }

    public void CreateRingImages()
    {
        _ringsCount = GameObject.FindGameObjectsWithTag("Ring").Length;

        for (int i = 0; i < _ringsCount; i++)
        {
            if (_ringCounter != null)
            {
                var newRingImage = Instantiate(_ringImage, _ringCounter);
                newRingImage.transform.Translate((_ringWidth + _ringWidth / 5) * _ringImages.Count, 0, 0);
                _ringImages.Add(newRingImage);
            }
        }
    }

    public void AddRingToScore()
    {
        jumpSound.Play();
        _ringsCount--;
        Destroy(_ringImages[(_ringImages.Count) - 1]);
        _ringImages.Remove(_ringImages[_ringImages.Count - 1]);
    }
}
