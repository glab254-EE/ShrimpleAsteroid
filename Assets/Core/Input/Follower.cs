using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private bool AutomaticOffset = false;
    [SerializeField]
    private bool HorizontalOnly = false;
    [SerializeField]
    private Transform Target;
    private Vector3 Offset;
    void Start()
    {
        Offset = AutomaticOffset ? transform.position - Target.position : new();
    }
    void Update()
    {
        if (HorizontalOnly)
        {
            transform.position = new(Target.position.x + Offset.x, transform.position.y, transform.position.z);
        } else
        {
            transform.position = Target.position + Offset;
        }
    }
}
