using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMover : MonoBehaviour
{
    [Header("Player Things")]
    [SerializeField] private Rigidbody myRb;
    [SerializeField] private ParticleSystem emitter;
    [SerializeField] private Transform initTransform;

    [Header("Move Things")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float acceleration;

    [SerializeField] private Animator myAnimator;

    private Vector2 moveVec;

    public bool controllsEnabled { get; private set; } = false;
    private Vector3 initScale;
    private void Awake()
    {
        initTransform.position = transform.position;
        initTransform.rotation = transform.rotation;
        initScale = transform.localScale;

        initTransform.parent = null;
    }
    private void Start()
    {
        AudioManager.Instance.PlayBoatSounds();
    }
    public void OnMove(InputAction.CallbackContext value)
    {
        if (controllsEnabled)//value.phase == InputActionPhase.Performed)
        {
            Vector2 inputVec = value.ReadValue<Vector2>();

            moveVec = new Vector2(-(inputVec.y), inputVec.x);

            if(moveVec.x > 0)
            {
                moveVec.x *= 0.3f;
            }

            if(moveVec.sqrMagnitude > 1)
            {
                moveVec = moveVec.normalized;
            }

            myAnimator.SetFloat("Y", inputVec.y);

            transform.Rotate(Vector3.up, rotationSpeed * inputVec.y);

            //if (myRb.linearVelocity.magnitude < maxVelocity)
            //{
            //    myRb.AddForce(transform.forward * acceleration * inputVec.x, ForceMode.Acceleration);
            //}
        }
    }

    private void FixedUpdate()
    {
        myAnimator.speed = Mathf.Clamp(Mathf.Lerp(myAnimator.speed, moveVec.magnitude, 0.5f), 0, 100);

        transform.Rotate(Vector3.up, rotationSpeed * moveVec.y);
        if (moveVec.x != 0 || moveVec.y != 0)
        {
            if (!emitter.isEmitting) {
                var em = emitter.emission;
                em.enabled = true;
            }
        }
        else
        {
            if (emitter.isEmitting)
            {
                var em = emitter.emission;
                em.enabled = false;
            }
        }
        myRb.AddTorque(0, moveVec.y * rotationSpeed, 0);
        if (myRb.linearVelocity.magnitude < maxVelocity)
        {
            myRb.AddForce(transform.forward * acceleration * moveVec.x * Time.fixedDeltaTime, ForceMode.Force);
        }

        if(myRb.linearVelocity.magnitude > 0.2)
        {
            AudioManager.Instance.SetBoatPitch(myRb.linearVelocity.magnitude /4);
        }
        else
        {
            AudioManager.Instance.SetBoatPitch(0);
        }
    }

    public void EnableControlls(bool enabled)
    {
        controllsEnabled = enabled;
        GetComponent<TryInteract>().EnableControlls(enabled);
        if (!enabled)
        {
            moveVec = Vector3.zero; 
            myRb.linearVelocity = Vector3.zero;
            myRb.angularVelocity = Vector3.zero;
        }

        //myRb.isKinematic = !enabled;
    }

    public void ResetPlayer()
    {
        transform.SetPositionAndRotation(initTransform.position, initTransform.rotation);
        transform.localScale = initScale;
        myRb.angularVelocity = Vector3.zero;
        myRb.linearVelocity = Vector3.zero;

        AudioManager.Instance.PlayMusic("Music");
        AudioManager.Instance.PlayBoatSounds();
    }
}
