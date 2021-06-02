using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnvironment : MonoBehaviour
{
    static WorldEnvironment instance = null;
    [SerializeField] float gravity = 0.981f;    public static float Gravity => instance.gravity;

    void Awake()
    {
        instance = this;
    }
}
