using UnityEngine;

public class Levelenemy : MonoBehaviour
{
    private void OnDisable()
    {
        if(!gameObject.scene.isLoaded || this == null)
        {
            return;
        }
        Levelmanagement.levelmanagement.Progress();
    }
}
