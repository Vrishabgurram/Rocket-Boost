using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Component References")]
    private Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sfxThrust;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem vfxThrust;
    [SerializeField] private ParticleSystem vfxLeftThrust;
    [SerializeField] private ParticleSystem vfxRightThrust;

    [Header("Power Values")]
    [SerializeField] private float thrustPower;
    [SerializeField] private float rotationPower;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            //Debug.Log("Space is pressed");
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sfxThrust);
            }
            if (!vfxThrust.isPlaying)
            {
                vfxThrust.Play();
            }
            rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
            vfxThrust.Stop();
        }
    }

    private void Update()
    {
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationValue = rotation.ReadValue<float>();
        if(rotationValue != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(rotationValue * Vector3.forward * rotationPower * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if(rotationValue == 1)
        {
            vfxLeftThrust.Play();
        }
        if(rotationValue == -1)
        {
            vfxRightThrust.Play();
        }
        else
        {
            vfxLeftThrust.Stop();
            vfxRightThrust.Stop();
        }

    }
}