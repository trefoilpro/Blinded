using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private List<CallInteraction> _callInteractionObjectsList;
    private bool _isOpened;

    private void Start()
    {
        foreach (var callInteractionObject in _callInteractionObjectsList)
        {
            callInteractionObject.OnInteractionWithObject += OnInteractionWithObject;
        }
        //_callInteractionObjectsList[0].OnInteractionWithObject += OnInteractionWithObject;
    }

    private void OnInteractionWithObject(object sender, EventArgs e)
    {
        ToggleDoorState();
    }

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
