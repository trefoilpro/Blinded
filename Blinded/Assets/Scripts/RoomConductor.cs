using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomConductor : MonoBehaviour
{
    /*public event EventHandler OnChangingRoomVariables;*/

    public Action<bool> RemakeMonstersEnviromentView;
    public Action<bool, bool> OnChangingRoomVariables;

    private void Awake()
    {
        OnChangingRoomVariables += ChangeRoomLayouts;
    }


    private void OnDisable()
    {
        OnChangingRoomVariables -= ChangeRoomLayouts;
    }
    
    private void ChangeRoomLayouts(bool isPlayerInside, bool isAnyOpendDoors)
    {
        Debug.Log("ChangeRoomLayouts");
        if (isPlayerInside && !isAnyOpendDoors)
        {
            RemakeMonstersEnviromentView?.Invoke(false);
            
        }
        else
        {
            RemakeMonstersEnviromentView?.Invoke(true);
        }
        
        
    }
}
