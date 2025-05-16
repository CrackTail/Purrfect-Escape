using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera SecondCamera;

    private bool usingMainCamera = true;

    void Start()
    {
        MainCamera.enabled = true;
        SecondCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            usingMainCamera = !usingMainCamera;
            MainCamera.enabled = usingMainCamera;
            SecondCamera.enabled = !usingMainCamera;
        }
    }
}
