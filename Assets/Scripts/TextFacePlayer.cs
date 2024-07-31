using UnityEngine;

public class TextFacePlayer : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if(cam != null)
        {
            Vector3 direction = cam.transform.position - transform.position;
            direction.y = 0; // Keep the text upright by ignoring the y-axis rotation

            // Rotate the text to face the player
            transform.rotation = Quaternion.LookRotation(-direction);
        }
            
    }
}
