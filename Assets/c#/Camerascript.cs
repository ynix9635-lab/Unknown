using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camerascript : MonoBehaviour
{
    Camera cam;
    public static Camerascript camerascript;
    [SerializeField]LayerMask POV;
    [SerializeField]LayerMask regularmask;
    private void Awake()
    {
        camerascript = this;
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
