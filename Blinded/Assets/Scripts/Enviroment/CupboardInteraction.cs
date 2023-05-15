using UnityEngine;

public class CupboardInteraction : CallInteraction
{
    [SerializeField] private CupboardToggle _cupboardToggle;
    public override void Interact()
    {
        base.Interact();
        _cupboardToggle.OnInteractionWithCupboard();
    }
}