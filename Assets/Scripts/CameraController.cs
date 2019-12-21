using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float2 bounds;
    public float speed;

    private void FixedUpdate() {
        Vector3 pos = Vector3.Lerp(transform.position, target.position, speed * Time.fixedDeltaTime);
        pos.x = Mathf.Clamp(pos.x, -bounds.x * 0.5f, bounds.x * 0.5f);
        pos.y = Mathf.Clamp(pos.y, -bounds.y * 0.5f, bounds.y * 0.5f);
        pos.z = -10;
        transform.position = pos;
    }
    
    private void OnDrawGizmos() {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(bounds.x, bounds.y, 0));
    }
}