using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ebutton : MonoBehaviour
{
    public static Ebutton reference;
    private void Awake()
    {
        reference = this;
    }
}
