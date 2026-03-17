using System.Collections;
using UnityEngine;

public class MetalFire : InteractableItem
{
    [Header("基礎設定")]
    public ParticleSystem FireFX;       // 原始火勢粒子
    public AudioSource FireSound;
    public ParticleSystem ExplosionFX; // 金屬火遇水噴發的特效 (請在 Inspector 拖入)

    [Header("提示訊息")]
    public Hint HydrantHint;      // 提示：需要消防栓
    public Hint MetalFireWaterHint;    // 提示：金屬火災不可用水！
    public Hint ExtinguisherHint; // 提示：金屬火需要滅火器

    [Header("火源屬性")]
    public bool isMetalFire;      // 勾選代表這是金屬火 (特殊火源 1-4)
    private bool hasExploded = false;

    public override void Interact(PlayerInteraction player)
    {
        if (player.TryGetComponent<PlayerAction>(out var playerAction))
        {
            // --- 情況 1：玩家拿著消防栓噴嘴 (水) ---
            if (playerAction.holdNozzle)
            {
                // 先讓玩家執行噴水動作 (視覺表現)
                playerAction.HydrantNozzle(); 

                if (isMetalFire)
                {
                    // 如果是金屬火，觸發變大反應，但不執行熄滅
                    if (!hasExploded) StartCoroutine(MetalFireReaction());
                }
                else
                {
                    // 如果是普通火，執行熄滅
                    StartCoroutine(PutOutFire(3f)); 
                }
            }
            // --- 情況 2：玩家拿著滅火器 ---
            // 這裡判斷 holdNozzle 為 false 且符合滅火器道具需求
            else if (playerAction.holdExtinguisher)
            {
                // 執行滅火器噴射動作
                playerAction.Extinguish(); 
                // 不論哪種火，滅火器都能熄滅它
                StartCoroutine(PutOutFire(3f)); 
            }
            // --- 情況 3：工具不對或沒拿工具 ---
            else
            {
                // 根據火的種類給予正確提示
                if (isMetalFire)
                    ShowHint(ExtinguisherHint); 
                else
                    ShowHint(HydrantHint);
            }
        }
    }

    // 金屬火遇水的劇烈反應
    IEnumerator MetalFireReaction()
    {
        hasExploded = true;
        if (MetalFireWaterHint != null)
        {
            ShowHint(MetalFireWaterHint);
        }

        // 1. 播放爆炸/強光特效
        if (ExplosionFX != null) ExplosionFX.Play();

        // 3. 視覺加強：讓火變得瘋狂
        var emission = FireFX.emission;
        emission.rateOverTime = 500f; // 增加粒子密度

        var main = FireFX.main;
        main.startSize = 15f;        // 增加尺寸
        main.startColor = Color.white; // 變白熱化

        Debug.Log("金屬火災用水反應：火勢劇增！");
        yield return new WaitForSeconds(1.5f);
        
        // 2. 讓原本的火焰變得極大且發白 (模擬高溫)
       /*var main = FireFX.main;
        main.startSize = 12f; 
        main.startColor = new Color(2.5f, 2.5f, 2.5f, 1f); // HDR 強白光

        // 3. 增加環境音效音量 (模擬化學劇烈聲)
        if (FireSound != null) FireSound.pitch = 1.5f;

        yield return new WaitForSeconds(1.5f);*/
        // 注意：這裡不呼叫 Stop()，所以火會一直燒下去
    }

    // 熄滅火源的通用協程
    IEnumerator PutOutFire(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (FireFX != null) FireFX.Stop();
        if (FireSound != null) FireSound.Stop();
        
        // 關閉碰撞體，讓玩家可以通過
        var col = GetComponent<BoxCollider>();
        if (col != null) col.enabled = false;
    }

    // 傷害觸發維持不變
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerHealth>(out var hp))
        {
            hp.TakeDamage(1, DamageType.Fire);
        }
    }

    public override void InteractSound() { }
}