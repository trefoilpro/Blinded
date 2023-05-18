using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blind : MonoBehaviour
{
    [SerializeField] private Image _blindnessImage;
    [SerializeField] private Image _blindnessImage2;
    [SerializeField] private int _waitSeconds;
    [Range(0,1)] [SerializeField] private float _worsenBy;
    private Color currentColor;
    private Color currentColor2;
    private void Start()
    {
        currentColor = new Color(0, 0, 0, 0);
        currentColor2 = new Color(0, 0, 0, 0);
        _blindnessImage.color = currentColor;
        _blindnessImage2.color = currentColor2;
        StartCoroutine(WorsenVision());
    }

    IEnumerator WorsenVision()
    {
        yield return new WaitForSeconds(_waitSeconds);
        currentColor += new Color(0, 0, 0, _worsenBy);
        currentColor2 += new Color(0, 0, 0, _worsenBy * .8f);
        _blindnessImage.color = currentColor;
        _blindnessImage2.color = currentColor2;
        StartCoroutine(WorsenVision());
    }
}
