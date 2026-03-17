using UnityEngine;

public class MetalFire1 : MonoBehaviour
{
    public ParticleSystem baseFire;       // 初始火勢
    public ParticleSystem explosionEffect; // 遇水爆炸效果
    public Light fireLight;               // 火光

    void OnParticleCollision(GameObject other)
    {
        // 假設水分身的粒子系統標籤為 "Water"
        if (other.CompareTag("Water"))
        {
            TriggerReaction();
        }
    }

    void TriggerReaction()
    {
        // 1. 停掉原本的小火，或大幅增加其發射量
        var main = baseFire.main;
        main.startSize = 10f; 

        // 2. 播放爆炸特效
        if (!explosionEffect.isPlaying) explosionEffect.Play();

        // 3. 瞬間增強光照強度
        fireLight.intensity = 50f;
        fireLight.range = 20f;
    }
}