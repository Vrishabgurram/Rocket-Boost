using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip sfxCrash;
    [SerializeField] private AudioClip sfxFinish;

    [SerializeField] private ParticleSystem vfxCrash;
    [SerializeField] private ParticleSystem vfxFinish;

    [SerializeField] float delayTime = 2f;

    bool isControllable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isControllable = !isControllable;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isControllable)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly object:" + collision.gameObject.name);
                break;
            case "Fuel":
                Debug.Log("Fuel object");
                break;
            case "Finish":
                Debug.Log("Level Complete");
                StartFinishSequence();
                break;
            default:
                Debug.Log("Explode");
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isControllable = true;
        vfxCrash.Play();
        audioSource.PlayOneShot(sfxCrash);
        GetComponent<NewMonoBehaviourScript>().enabled = false;
        Invoke("ReloadScene", delayTime);
    }

    private void StartFinishSequence()
    {
        isControllable = true;
        vfxFinish.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sfxFinish);
        GetComponent<NewMonoBehaviourScript>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    private void ReloadScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex);
    }

    private void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        if (currentIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextIndex = 0;
        }
        SceneManager.LoadScene(nextIndex);
    }
}
