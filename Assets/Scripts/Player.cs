using UnityEngine;

public class BananaMan : MonoBehaviour
{
    public Transform startingPosition; // Initial player position
    public float minYPosition = -5f; // Minimum y-coordinate below which the player falls
    public float resetDelay = 1f; // Delay before resetting the player after falling
    public bool isFalling = false; // Flag to track if the player is currently falling

    private Vector3 lastCoinPosition; // Last collected coin position

    private Rigidbody rigidBodyComponent;
    private Timer timerScript;
    private CapsuleCounter capsuleCounter; // Dodajte referencu na CapsuleCounter klasu
    bool jump;
    bool touchGround;
    private float horizontal;

    public float stopXCoordinate = 8.53f; // Adjust this value to the desired X coordinate

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        jump = false;
        timerScript = FindObjectOfType<Timer>(); // Assuming Timer is a singleton, adjust accordingly
        capsuleCounter = FindObjectOfType<CapsuleCounter>(); // Pronalazi referencu na CapsuleCounter
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && touchGround)
        {
            jump = true;
        }
        horizontal = Input.GetAxis("Horizontal");
        
    }

    void FixedUpdate()
    {
        if (jump)
        {
            rigidBodyComponent.AddForce(7 * Vector3.up, ForceMode.VelocityChange);
            jump = false;
            touchGround = false;
        }
        rigidBodyComponent.velocity = new Vector3(horizontal * 2, rigidBodyComponent.velocity.y, 0);

        // Check if the player has fallen below minYPosition
        if (transform.position.y < minYPosition && !isFalling)
        {
            // Set the falling flag and start the reset countdown
            isFalling = true;
            Invoke("ResetPlayerPosition", resetDelay);
        }

        // Check if the player has reached stopXCoordinate and update the timer
        if (transform.position.x >= stopXCoordinate)
        {
            UpdateTimer();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        touchGround = true;
    }

    void OnTriggerEnter(Collider other)
    {
        soundManager.instance.coinssource.PlayOneShot(soundManager.instance.coinSound);
        Destroy(other.gameObject);
        CoinCollected(other.transform.position);
        capsuleCounter.AddDestroyedCapsule(); // Dodaje uništenu kapsulu u brojaè
    }

    void CoinCollected(Vector3 coinPosition)
    {
        lastCoinPosition = coinPosition;
        // Insert any logic related to coin collection here
    }

    void ResetPlayerPosition()
    {
        // Reset the player's position to the position of the last collected coin
        transform.position = lastCoinPosition;
        isFalling = false; // Reset the falling flag
    }

    void UpdateTimer()
    {
        if (timerScript != null)
        {
            if (transform.position.x >= stopXCoordinate)
            {
                Debug.Log("Player reached stopXCoordinate. Pausing timer.");
                timerScript.PauseTimer();
            }
            else
            {
                Debug.Log("Player not at stopXCoordinate. Resuming timer.");
                timerScript.ResumeTimer();
            }
        }
    }
}