using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class Povcamerasensitivity : MonoBehaviour
{
    const float basesensitivity = 600f;
    CinemachineVirtualCamera povcamera;
    CinemachinePOV pov;
    static public Povcamerasensitivity povcamerasensitivity;
    private void Awake()
    {
        povcamerasensitivity = this;
    }
    void Start()
    {
        povcamera = GetComponent<CinemachineVirtualCamera>();
        pov = povcamera.GetCinemachineComponent<CinemachinePOV>();
    }
    public void Setsensitivity(float sensitivitymultiplier)
    {
        pov.m_HorizontalAxis.m_MaxSpeed = sensitivitymultiplier*basesensitivity;
        pov.m_VerticalAxis.m_MaxSpeed = sensitivitymultiplier * basesensitivity;
    }
}
