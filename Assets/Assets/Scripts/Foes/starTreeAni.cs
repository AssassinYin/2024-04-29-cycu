using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class starTreeAni : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject tree;

    private starTree treeCon;
    // Start is called before the first frame update
    void Start()
    {
        treeCon = tree.GetComponent<starTree>(); 
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found.");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void preTothrowAttack()
    {
        animator.Play( "starTreeThrow" );
    }

    public void throwStar() {
        treeCon.throwAttack();
        animator.Play("starTreeStand");
  
    }

    public void spin() {
        animator.Play("starTreeStand");
    }

    public void preToSpin() {
        animator.Play( "spinning" );
    }

    public void spinToEnd() {
        animator.Play( "spinEnd" );
    }

    public void dig() {
        treeCon.digAttack();
        animator.Play("starTreeStand");
 
    }


}
