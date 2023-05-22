using Enviroment;
using UnityEngine;

public class WoodenWardrobeInteraction : CallInteraction
{
    [SerializeField] private WoodenWardrobeToggle _woodenWardrobeToggle;
    public override void Interact()
    {
        base.Interact();
        _woodenWardrobeToggle.OnInteractionWithWardrobe();
    }
}