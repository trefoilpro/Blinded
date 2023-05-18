using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blind : MonoBehaviour
{
    [SerializeField] private Image _blindnessImage;

    private void Start()
    {
        _blindnessImage.color = new Color(0, 0, 0, 0);
    }

    IEnumerator WorsenVision()
    {

        yield return WaitForSeconds();
    }
}
