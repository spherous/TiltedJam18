using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : MonoBehaviour, IPooledObject<Snowglobe>
{
    public Action<Snowglobe> ReturnToPool { get; set; }
    
    public float effectDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        Santa s = other.gameObject.GetComponent<Santa>();
        if(s != null)
            StartCoroutine(KillAllZombies());
    }
    IEnumerator KillAllZombies(){
        float t = 0f;
        Vector3 startScale = transform.localScale;
        while(t < effectDelay)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, transform.localScale * 1.08f, t/effectDelay);
            yield return null;
        }
        Collider2D[] cols = Physics2D.OverlapCircleAll(Vector2.zero,1000f);
        foreach(Collider2D c in cols)
        {
            Enemy e = c.gameObject.GetComponent<Enemy>();
            e?.TakeDamage(e.currentHealth);
        }
        transform.localScale = startScale;
        ReturnToPool(this);
    }
}
