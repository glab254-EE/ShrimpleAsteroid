using UnityEngine.Events;
using UnityEngine.InputSystem;

public interface IInputReciever
{
    bool IsHeldDown { get; set; }
    object Value { get; set; }
    UnityEvent OnRecieve {  get; set; }
    void Recieve(InputAction.CallbackContext callback)
    {
        IsHeldDown = callback.performed;

        Value = callback.ReadValueAsObject();

        OnRecieve?.Invoke();
    }
}
