using UnityEngine;

public class ShootingRay : MonoBehaviour
{
    LineRenderer lr;
    void Start() { lr = GetComponent<LineRenderer>(); }
    void Update()
    {
        lr.SetPosition(0, transform.position); // Start at controller
        lr.SetPosition(1, transform.position + (transform.forward * 10f)); // End 10m away
    }
}
