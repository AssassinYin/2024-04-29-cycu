using UnityEngine;


public class starTreeAni : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject tree;

    private StarTree treeCon;
    // Start is called before the first frame update
    void Start()
    {
        treeCon = tree.GetComponent<StarTree>(); 
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
        // �����𭵮�
        if (SoundManager.instance != null)
        {
            SoundManager.instance.startree_PlayAttackSound("threestar");
        }
        else
        {
            Debug.LogWarning("SoundManager instance not found!");
        }


    }


}
