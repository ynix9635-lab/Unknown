using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class Xfreecamerasensitivity : MonoBehaviour
{
    const float basesensitivity = 600f;
    static public Xfreecamerasensitivity xfreecamerasensitivity;
    CinemachineVirtualCamera xfreelookcamera;
    CinemachineOrbitalTransposer orbitalTransposer;
    void Awake()
    {
        xfreecamerasensitivity = this;
        xfreelookcamera = GetComponent<CinemachineVirtualCamera>();
        orbitalTransposer = xfreelookcamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }
    public void Setsensitivity(float sensitivitymultiplier)
    {
        orbitalTransposer.m_XAxis.m_MaxSpeed = sensitivitymultiplier * basesensitivity;
    }
}
