using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSanta : MonoBehaviour
{
    private Santa santa;

    private bool santaDied = false;

    private void Awake()
    {
        santa = FindObjectOfType<Santa>();
        if (!santa) Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = santa.transform.position;

        if (santa.currentHealth <= 0)
        {
            santaDied = true;
            StartCoroutine(ShrinkAndDie());
        }
    }

    IEnumerator ShrinkAndDie ()
    {
        yield return null;
    }
}
