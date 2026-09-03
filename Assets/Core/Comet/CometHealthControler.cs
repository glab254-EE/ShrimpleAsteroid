using UnityEngine;
using UnityEngine.Events;

public class CometHealthControler : MonoBehaviour
{
    public float Health { get; private set; }
    [field:SerializeField]
    public float MaxHealth { get; private set; }
    [HideInInspector]
    public UnityEvent<float> OnHealthChange;
    void Start()
    {
        Health = MaxHealth;
    }
    public void DamagePlayer(float damage)
    {
        Health = Mathf.Clamp(Health - Mathf.Abs(damage), 0, MaxHealth);
        OnHealthChange?.Invoke(Health);
    }
}
