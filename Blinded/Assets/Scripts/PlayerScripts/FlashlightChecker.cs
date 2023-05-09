using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightChecker : MonoBehaviour
{
    [SerializeField] private GameObject _flashlightGameObject;
    [SerializeField] private KeyCode _flashlightKey = KeyCode.E;
    private bool _canSwitchFlashlight = true;
    private bool _isGlowing = true;

    private void Update()
    {
        if (Input.GetKeyDown(_flashlightKey)) CheckFlashlight();
    }

    private void CheckFlashlight()
    {
        if (_canSwitchFlashlight)
        {
            if (_isGlowing)
            {
                SetActiveFlashLight(false);
            }
            else
            {
                SetActiveFlashLight(true);
            }
        }
    }

    private void SetActiveFlashLight(bool variable)
    {
        _isGlowing = variable;
        _flashlightGameObject.SetActive(_isGlowing);
    }
}
