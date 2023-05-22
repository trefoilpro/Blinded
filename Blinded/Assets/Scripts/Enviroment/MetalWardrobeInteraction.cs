using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalWardrobeInteraction : CallInteraction
{
    [SerializeField] private MetalWardrobeToggle _metalWardrobeToggle;
    public override void Interact()
    {
        base.Interact();
        _metalWardrobeToggle.OnInteractionWithWardrobe();
    }
}
