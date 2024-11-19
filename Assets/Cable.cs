using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public Animator animator { get; private set; }
    public Collider2D Collider2D { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Collider2D = GetComponent<Collider2D>();
    }

    public void OpenCable()
    {
        animator.CrossFade("lightning", 0.1f);
        Collider2D.enabled = true;
        StartCoroutine(CloseCableRoutine());
    }

    private IEnumerator CloseCableRoutine() { 
        yield return new WaitForSeconds(0.12f);
        CloseCable();
    }

    public void CloseCable()
    {
        animator.CrossFade("lightning_close", 0.1f);
        Collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerHealth>() != null)
        {
            col.GetComponent<PlayerHealth>().ApplyDamage(10);
        }
    }
}
