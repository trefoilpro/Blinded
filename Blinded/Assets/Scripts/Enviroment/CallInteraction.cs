using UnityEngine;

public class CallInteraction : MonoBehaviour
{
    /*public event EventHandler OnInteractionWithObject;*/
    //[SerializeField] private GameObject scriptHolderObj;

    public virtual void Interact()
    {
        Debug.Log("Interact");
        /*OnInteractionWithObject?.Invoke(this, EventArgs.Empty);
        Debug.Log("Interact");*/
    }
    
    
    
    
    /*public void Interact()
    {
        scriptHolderObj.BroadcastMessage("ToggleInteraction");
    }*/
    
}