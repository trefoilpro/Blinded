using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    private bool _isOpened;
    [SerializeField] private Animator doorAnimator;

    public void ToggleInteraction()
    {
        ToggleDoorState();
    }
    private void ToggleDoorState()
    {
        if (_isOpened) _isOpened = false;
        else _isOpened = true;

        doorAnimator.SetBool("isOpen", _isOpened);
    }
}
