using UnityEngine;

public class Camerascript : MonoBehaviour
{
    Camera cam;
    public static Camerascript camerascript;
    [SerializeField]LayerMask POV;
    [SerializeField]LayerMask regularmask;
    private void Awake()
    {
        camerascript = this;
    }
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    public void SwitchPOVmode(bool isfirstperson)
    {
        if (isfirstperson)
        {
            cam.cullingMask = POV;
        }
        else
        {
            cam.cullingMask = regularmask;
        }
    }
}
