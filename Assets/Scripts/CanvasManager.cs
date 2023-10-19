using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Static reference to the instance
    public static CanvasManager Instance { get; private set; }

    // The Transform of the Canvas component
    public Transform CanvasTransform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        // You can also directly set the CanvasTransform here if you prefer
        // CanvasTransform = GetComponent<Transform>();
    }
}
