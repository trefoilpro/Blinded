using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardToggle : MonoBehaviour
{
    /*
    [SerializeField] private List<CallInteraction> _callInteractionObjectsList;
    */

    /*private void Start()
    {
        foreach (var callInteractionObject in _callInteractionObjectsList)
        {
            callInteractionObject.OnInteractionWithObject += OnInteractionWithObject;
        }
        //_callInteractionObjectsList[0].OnInteractionWithObject += OnInteractionWithObject;
    }*/

    /*private void OnInteractionWithObject(object sender, EventArgs e)
    {
        
        Player.Instance.SetHidden(transform);
    }*/

    public void OnInteractionWithCupboard()
    {
        Player.Instance.SetHidden(transform);
    }

    /*private void OnDisable()
    {
        foreach (var callInteractionObject in _callInteractionObjectsList)
        {
            callInteractionObject.OnInteractionWithObject -= OnInteractionWithObject;
        }
    }*/
}
