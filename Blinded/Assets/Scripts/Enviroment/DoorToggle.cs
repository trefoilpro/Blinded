using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private Room _room;

    public bool IsOpened { get; private set; } = false;
    

    public void ToggleInteraction()
    {
        ToggleDoorState();
    }
    private void ToggleDoorState()
    {
        if (IsOpened) IsOpened = false;
        else IsOpened = true;

        if (_room != null)
        {
            _room.OnDoorChange();
        }

        doorAnimator.SetBool("isOpen", IsOpened);
    }
}
