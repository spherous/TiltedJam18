using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    // [SerializeField]
    // private float maxSpeed;
    [SerializeField]
    private float speed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // if(Mathf.Approximately(x, 0) && Mathf.Approximately(y, 0))
        //     rb.velocity = new float2(0,0);

        rb.AddForce(new float2(x,y) * speed * Time.deltaTime);
    }
}
