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
        fireAtLocation.z = -1;
        Debug.DrawRay(transform.position, fireAtLocation - transform.position, Color.red, 1f);
        transform.up = fireAtLocation - transform.position;
        Debug.DrawRay(transform.position, transform.up, Color.green, 1f);
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
}
