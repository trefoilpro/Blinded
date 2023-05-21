using Enviroment;
using UnityEngine;

public class WardrobeInteraction : CallInteraction
{
    [SerializeField] private WardrobeToggle _wardrobeToggle;
    public override void Interact()
    {
        base.Interact();
        _wardrobeToggle.OnInteractionWithWardrobe();
    }
}