using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSender : MonoBehaviour
{
    private Dictionary<InputAction, List<IInputReciever>> Recievers = new();
    void OnDestroy()
    {
        foreach(var action in Recievers.Keys)
        {
            action.performed -= ValidateAction;
            action.canceled -= ValidateAction;
            if (!action.enabled) continue;
            action.Disable();
        }
    }
    public bool TryAddReciever(InputAction action, IInputReciever reciever)
    {
        if (Recievers.ContainsKey(action))
        {
            var vals = Recievers[action];
            if (vals.Contains(reciever)) return false;
            Recievers[action].Add(reciever);
        }
        else
        {
            Recievers.Add(action, new() { reciever });
            action.performed += ValidateAction;
            action.canceled += ValidateAction;
            if (!action.enabled) action.Enable();
        }
        return true;
    }
    void ValidateAction(InputAction.CallbackContext context)
    {
        if (Recievers.TryGetValue(context.action,out var recievers))
        {
            foreach(IInputReciever reciever in recievers)
            {
                reciever.Recieve(context);
            }
        }
    }
}
