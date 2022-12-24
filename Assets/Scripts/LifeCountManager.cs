using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCountManager : MonoBehaviour
{
    private const int _baseLifeCount = 3; // Можно использовать byte;
    [SerializeField] private Text _lifeCountText;
    private static int _lifeCount;

    void Start()
    {
        _lifeCount = _baseLifeCount;
        //_lifeCountText = GameObject.FindGameObjectWithTag("LifeCountText").GetComponent<Text>();
    }

    void Update()
    {
        SetCountText();
    }

    static internal void ChangeCountTo(int count)
    {
        _lifeCount += count;
    }

    static internal int GetCount()
    {
        return _lifeCount;
    }

    private void SetCountText()
    {
        _lifeCountText.text = "X" + _lifeCount.ToString();
    }
}
