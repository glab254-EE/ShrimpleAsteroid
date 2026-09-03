using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CometMovementController : MonoBehaviour, IInputReciever
{
    [SerializeField]
    private float MaxHorizontalSpeed = 5f;
    [SerializeField]
    private float MaxVerticalSpeed = 5f;
    [SerializeField]
    private float VelocityChangeSpeed = 1.0f;
    [SerializeField]
    private CometHealthControler healthControler;
    [SerializeField]
    private InputSender InputSender;
    [SerializeField]
    private InputActionReference InputActionReference;
    public bool IsHeldDown { get; set; } = false;
    public object Value { get; set; } = null;
    public UnityEvent OnRecieve { get; set; } = new();
    private bool IsPlaying = false;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        InputSender.TryAddReciever(InputActionReference.action, this);
        OnRecieve.AddListener(OnFirstInput);
    }
    void Update()
    {
        if (IsPlaying)
        {
            float TargetUpVelocity = IsHeldDown ? MaxVerticalSpeed : -MaxVerticalSpeed;
            Vector2 currrent = rb.linearVelocity;
            currrent = Vector2.Lerp(currrent, new(MaxHorizontalSpeed * (healthControler.Health/healthControler.MaxHealth), TargetUpVelocity), Time.deltaTime * VelocityChangeSpeed);
            rb.linearVelocity = currrent;
            if (healthControler.Health <= 0)
            {
                Debug.Log("Dead!");
                rb.linearVelocity = Vector2.zero;
                IsPlaying = false;
            }
        }
    }
    void OnFirstInput()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
        }
        OnRecieve.RemoveListener(OnFirstInput);
    }
}
