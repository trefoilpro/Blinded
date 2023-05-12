using UnityEngine;

public class DoorToggleInteraction : CallInteraction
{
    [SerializeField] private DoorToggle _doorToggle;
    public override void Interact()
    {
        base.Interact();
        _doorToggle.ToggleInteraction();
    }
}