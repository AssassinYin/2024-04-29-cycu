using UnityEngine;

public class Cal : MonoBehaviour
{
    public Transform Transform;
    public GameObject Pi;
    public GameObject Pi1, Pi2, Pi3;
    public GameObject Pi4, Pi5, Pi6;
    public GameObject Pi9;
    public GameObject PiDot;
    public GameObject BoolFunc;
    public GameObject BoolFuncLB, BoolFuncMB, BoolFuncRB;
    public GameObject Bomb;
    public Transform decidedTransform;

    public Transform decidedTransformUnder;
    public Transform decidedTransformRight;
    public Transform decidedTransformCenter;

    public Transform playerTrans;
    private float timer, delay;

    private void Awake()
    {
        timer = 0f;
        delay = Random.Range(1f, 4f);
        
        if ( GameObject.FindWithTag("Player") != null ) {
            playerTrans = GameObject.FindWithTag("Player").transform;
        }   
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to execute an action
        if (timer >= delay && 
            Mathf.Abs( this.transform.position.x - playerTrans.position.x ) < 80f && 
            Mathf.Abs( this.transform.position.y - playerTrans.position.y ) <= 60f )
        {
            // Pick a random action (0, 1, or 2)
            int action = Random.Range(0, 3);

            SelectAPoint();
            //BulletPointerHell();

            // Execute the chosen action
            
            switch (action)
            {
                case 0:
                    Debug.Log("Action 1 executed");
                    BulletPointerHell();
                    break;
                case 1:
                    Debug.Log("Action 2 executed");
                    BulletFuncHell();
                    break;
                case 2:
                    Debug.Log("Action 3 executed");
                    BulletPiHell();
                    break;
            }
            

            // Reset the timer and set a new random delay for the next action
            timer = 0f;
            delay = Random.Range(1f, 4f);
        }
    }

    public void SelectAPoint()
    {
        decidedTransform.position = GetRandomYPosition(Transform);
    }

    private Vector3 GetRandomYPosition(Transform originalTransform)
    {
        // Get the original position
        Vector3 originalPosition = originalTransform.position;

        // Generate a random offset between -4 and +4 for the y-axis
        float randomYOffset = Random.Range(-4f, 4f);

        // Return the new position with the random y-offset
        return new Vector3(originalPosition.x, originalPosition.y + randomYOffset, originalPosition.z);
    }

    public void BulletPointerHell()
    {

        // ���񭵮�
        SoundManager.instance.PlayCalBulletPointerHellSound();

        GameObject bulletInstance = Instantiate(Bomb, decidedTransform.position, Quaternion.identity);
        Pointer bulletComponent = bulletInstance.GetComponent<Pointer>();
        bulletComponent.dir = new Vector2(10, 4);

        bulletInstance = Instantiate(Bomb, decidedTransformRight.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pointer>();
        bulletComponent.dir = new Vector2(-10, 4);
    }

    public void BulletFuncHell()
    {
        // ���񭵮�
        SoundManager.instance.PlayCalBulletFuncHellSound();

        //left
        GameObject bulletInstance = Instantiate(BoolFunc, decidedTransformCenter.position, Quaternion.identity);
        BoolFunc bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(-10, 0);
        
        //bottom
        bulletInstance = Instantiate(BoolFuncMB, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(0, -10);
        //left bottom
        bulletInstance = Instantiate(BoolFuncLB, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(-5, -5);
        //right bottom
        bulletInstance = Instantiate(BoolFuncRB, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(5, -5);      
        //right
        bulletInstance = Instantiate(BoolFunc, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(10, 0);

    }

    public void BulletPiHell()
    {
        // ���񭵮�
        SoundManager.instance.PlayCalBulletPiHellSound();

        GameObject bulletInstance = Instantiate(Pi3, decidedTransformCenter.position, Quaternion.identity);
        Pi bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, 0);


        bulletInstance = Instantiate(PiDot, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, -6);

        bulletInstance = Instantiate(Pi1, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-6, -10);

        bulletInstance = Instantiate(Pi4, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(0, -10);

        bulletInstance = Instantiate(Pi1, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(6, -10);

        bulletInstance = Instantiate(Pi5, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(10, -6);

        bulletInstance = Instantiate(Pi9, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(10, 0);

        bulletInstance = Instantiate(Pi2, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(10, 6);

        bulletInstance = Instantiate(Pi6, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(6, 10);

        bulletInstance = Instantiate(Pi5, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(0, 10);

        bulletInstance = Instantiate(Pi3, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-6, 10);

        bulletInstance = Instantiate(Pi5, decidedTransformCenter.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, 6);
    }
}
