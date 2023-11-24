using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light ly;
    void Start()
    {
        ly.intensity = UnityEngine.Random.Range(0.3f, 1.4f); //change light intensity
    }

    // Update is called once per frame
    void Update()
    {
        ly.intensity = UnityEngine.Random.Range(0.3f, 1.4f); //change light intensity
    }
}
