using System.Collections;
using UnityEngine;

public class MetalFire : InteractableItem
{
    public ParticleSystem FireFX;       // 原始火勢
    public ParticleSystem ExplosionFX; // 遇水爆炸的特效 (新增)
    public Light FireLight;             // 燈光 (新增)
    public AudioSource FireSound;
    public Hint HydrantHint;

    public bool isMetalFire;            // 勾選代表這是金屬火災
    private bool isExploded = false;    // 確保只觸發一次爆炸

    public override void Interact(PlayerInteraction player)
    {
        if (player.TryGetComponent<PlayerAction>(out var playerAction))
        {
            // 情況 A：玩家拿著消防栓噴嘴 (水)
            if (playerAction.holdNozzle)
            {
                if (isMetalFire)
                {
                    // 關鍵邏輯：金屬火遇水變大
                    StartCoroutine(MetalFireReaction());
                }
                else
                {
                    // 普通火災遇水正常熄滅
                    playerAction.HydrantNozzle();
                    StartCoroutine(PutOutFire());
                }
            }
            // 情況 B：玩家使用滅火器 (假設 Extinguish 是滅火器動作)
            else if (CheckRequirements(player, out var inv))
            {
                playerAction.Extinguish();
                StartCoroutine(PutOutFire());
            }
            else
            {
                // 提示需要滅火器或去拿水管
                ShowHint(isMetalFire ? hint : HydrantHint);
            }
        }
    }

    // 金屬火災遇水的反應
    IEnumerator MetalFireReaction()
    {
        if (isExploded) yield break;
        isExploded = true;

        // 1. 播放爆炸特效與音效
        if (ExplosionFX != null) ExplosionFX.Play();
        
        // 2. 讓火勢粒子瞬間變大
        var main = FireFX.main;
        main.startSize = main.startSize.constant * 5f; // 火大 5 倍
        
        // 3. 燈光閃爍 (白熱化)
        if (FireLight != null) {
            FireLight.intensity = 50f;
            FireLight.color = Color.white;
        }

        // 4. (選配) 可以扣玩家血量，因為產生了化學爆炸
        Debug.Log("金屬火遇水！火勢劇增並引發爆炸！");

        yield return new WaitForSeconds(2f);
        // 之後火會維持在較大的狀態，直到被滅火器熄滅
    }

    IEnumerator PutOutFire()
    {
        yield return new WaitForSeconds(3f);
        if (FireFX != null) FireFX.Stop();
        if (FireSound != null) FireSound.Stop();
        var col = GetComponent<BoxCollider>();
        if (col != null) col.enabled = false;
        if (FireLight != null) FireLight.enabled = false;
    }

    // 原本的扣血邏輯維持不變
    void OnTriggerStay(Collider other)
    {
        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(1, DamageType.Fire);
        }
    }

    public override void InteractSound() { }
}