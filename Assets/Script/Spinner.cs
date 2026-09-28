using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 rotateAngles;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(rotateAngles);
    }
}
