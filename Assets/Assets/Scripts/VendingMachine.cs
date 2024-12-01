using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public Transform LeftTransform;
    public Transform LeftDecidedTransform;
    
    public Transform RightTransform;
    public Transform RightDecidedTransform;
    
    public GameObject NmlCan;
    public GameObject RegCan;
    public GameObject DeathCan;
    private float _timer, _delay;

    public Transform playerTrans;

    private int numOfCan;

    private void Awake()
    {
        _timer = 0f;
        _delay = Random.Range(0.5f, 1f);

        if ( GameObject.FindWithTag("Player") != null ) {
            playerTrans = GameObject.FindWithTag("Player").transform;
        }   

    }

    private void Update()
    {
        // Update the timer
        _timer += Time.deltaTime;

        // Check if it's time to execute an action
        if (_timer >= _delay && 
            Mathf.Abs( this.transform.position.x - playerTrans.position.x ) < 80f && 
            Mathf.Abs( this.transform.position.y - playerTrans.position.y ) <= 60f )
        {
            // Pick a random action (0, 1)
            int action = Random.Range(0, 3);

            SelectAPoint();

            // Execute the chosen action
            
            bool isLeft = Random.Range(0, 2) == 0;
            
            switch (action)
            {
                case 0:
                    Debug.Log("Action 1 executed");
                    SoundManager.instance.PlayVendingMachineSound(1); // ���񭵮ļҦ� 1
                    numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(NmlCan, isLeft);
                    }
                    numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(NmlCan, isLeft);
                    }
                     numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(NmlCan, isLeft);
                    }                  

                    break;
                case 1:
                    Debug.Log("Action 2 executed");
                    SoundManager.instance.PlayVendingMachineSound(2); // ���񭵮ļҦ� 2
                    numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(NmlCan, isLeft);
                    }
                    numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(RegCan, isLeft);
                    }

                    TossCan(DeathCan, isLeft);
                    
                    break;
                case 2:
                    Debug.Log("Action 3 executed");
                    SoundManager.instance.PlayVendingMachineSound(3); // ���񭵮ļҦ� 3
                    numOfCan = Random.Range( 1, 4 );
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(NmlCan, isLeft);
                    }
                    for ( int i = 0 ; i < numOfCan ; i++ ) {
                        TossCan(DeathCan, isLeft);
                    }

                    break;
            }
            
            // Reset the timer and set a new random delay for the next action
            _timer = 0f;
            _delay = Random.Range(1f, 4f);
        }
    }

    public void SelectAPoint()
    {
        LeftDecidedTransform.position = GetRandomYPosition(LeftTransform);
        RightDecidedTransform.position = GetRandomYPosition(RightTransform);
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

    private void TossCan(GameObject tGameObject, bool isLeft)
    {
        GameObject canInstance = Instantiate(tGameObject, isLeft ? LeftTransform.position : RightTransform.position, Quaternion.identity);
        Can canComponent = canInstance.GetComponent<Can>();
        canComponent.dir = isLeft ? new Vector2(Random.Range(-5, -11), Random.Range(0, 5)) : new Vector2(Random.Range(5, 11), Random.Range(0, 5));
    }
}
