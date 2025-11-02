using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    public Light warningLight;
    public float flashSpeed;
    public float rotateSpeed;
    public float minIntensity;
    public float maxIntensity;

    private void Update()
    {
        // 燈光強度閃爍
        float intensity = Mathf.PingPong(Time.time * flashSpeed, maxIntensity - minIntensity) + minIntensity;
        warningLight.intensity = intensity;

        // 只繞 Y 軸旋轉
        warningLight.transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
