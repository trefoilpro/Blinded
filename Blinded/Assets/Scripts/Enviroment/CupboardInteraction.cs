using UnityEngine;

public class CupboardInteraction : CallInteraction
{
    [SerializeField] private CupboardToggle _cupboardToggle;
    public override void Interact()
    {
        Debug.Log("Nuchaj Bebru");
        base.Interact();
        Debug.Log("Smacznego");
        _cupboardToggle.OnInteractionWithCupboard();
    }
}