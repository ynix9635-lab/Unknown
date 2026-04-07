using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class Freecamerasensitivity : MonoBehaviour
{
    CinemachineFreeLook freelookcamera;
    const float baseverticalsensitivity = 4f;
    const float basehorizontalsensitivity = 600f;
    public static Freecamerasensitivity freecamerasensitivity;
    void Awake()
    {
        freecamerasensitivity = this;
        freelookcamera = GetComponent<CinemachineFreeLook>();
    }
    public void Setsensitivity(float sensitivitymultiplier)
    {
        freelookcamera.m_YAxis.m_MaxSpeed = sensitivitymultiplier * baseverticalsensitivity;
        freelookcamera.m_XAxis.m_MaxSpeed = sensitivitymultiplier * basehorizontalsensitivity;
    }
}
