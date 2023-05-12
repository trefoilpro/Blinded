using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;

    public bool _isOpened { get; private set; } = false;
    

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
