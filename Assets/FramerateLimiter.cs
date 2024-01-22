using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    public void ChangeFramerate(int targetFPS)
    {
        Application.targetFrameRate = targetFPS;
    }
}
