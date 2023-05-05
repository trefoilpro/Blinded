using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallInteraction : MonoBehaviour
{
    [SerializeField] private DoorToggle scriptHolderObj;
    
    public void Interact()
    {
        scriptHolderObj.BroadcastMessage("ToggleInteraction");
    }
    
}
