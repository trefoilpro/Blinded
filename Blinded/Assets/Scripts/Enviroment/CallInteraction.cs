using System;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallInteraction : MonoBehaviour
{
    public event EventHandler OnInteractionWithObject;
    //[SerializeField] private GameObject scriptHolderObj;

    public void Interact()
    {
        OnInteractionWithObject?.Invoke(this, EventArgs.Empty);
        Debug.Log("Interact");
    }
    
    
    
    /*public void Interact()
    {
        scriptHolderObj.BroadcastMessage("ToggleInteraction");
    }*/
    
}
