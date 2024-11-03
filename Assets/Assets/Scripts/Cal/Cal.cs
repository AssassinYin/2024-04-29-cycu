using UnityEngine;

public class Cal : MonoBehaviour
{
    public Transform Transform;
    public GameObject Pi;
    public GameObject BoolFunc;
    public GameObject Bomb;
    public Transform decidedTransform;
    private float timer, delay;

    private void Awake()
    {
        timer = 0f;
        delay = Random.Range(1f, 4f);
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to execute an action
        if (timer >= delay)
        {
            // Pick a random action (0, 1, or 2)
            int action = Random.Range(0, 3);

            SelectAPoint();
            // Execute the chosen action
            
            switch (action)
            {
                case 0:
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
        GameObject bulletInstance = Instantiate(Bomb, decidedTransform.position, Quaternion.identity);
        Pointer bulletComponent = bulletInstance.GetComponent<Pointer>();
        bulletComponent.dir = new Vector2(-10, 4);
    }

    public void BulletFuncHell()
    {
        GameObject bulletInstance = Instantiate(BoolFunc, decidedTransform.position, Quaternion.identity);
        BoolFunc bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(-10, 0);

        bulletInstance = Instantiate(BoolFunc, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(-10, 3);

        bulletInstance = Instantiate(BoolFunc, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<BoolFunc>();
        bulletComponent.dir = new Vector2(-10, -3);
    }

    public void BulletPiHell()
    {
        GameObject bulletInstance = Instantiate(Pi, decidedTransform.position, Quaternion.identity);
        Pi bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, 0);

        bulletInstance = Instantiate(Pi, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, 3);

        bulletInstance = Instantiate(Pi, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, -3);

        bulletInstance = Instantiate(Pi, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-1, 7);

        bulletInstance = Instantiate(Pi, decidedTransform.position, Quaternion.identity);
        bulletComponent = bulletInstance.GetComponent<Pi>();
        bulletComponent.dir = new Vector2(-10, -7);
    }
}
