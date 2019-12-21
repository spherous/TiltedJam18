using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    private void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 fireAtLocation)
    {
        fireAtLocation.z = 0;
        Debug.DrawRay(transform.position, fireAtLocation - transform.position, Color.red, 1f);
        transform.forward = fireAtLocation - transform.position;
        rb.AddForce(transform.forward * force, ForceMode2D.Impulse);
    }
}
