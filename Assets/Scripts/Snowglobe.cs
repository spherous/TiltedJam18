using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : MonoBehaviour, IPooledObject<Snowglobe>
{
    public Action<Snowglobe> ReturnToPool { get; set; }
    
    public float effectDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(KillAllZombies());
    }
    IEnumerator KillAllZombies(){
        float t = 0f;
        Vector3 startScale = transform.localScale;
        while(t < effectDelay)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, transform.localScale * 1.1f, t/effectDelay);
            yield return null;
        }
        
        ReturnToPool(this);
    }
}
