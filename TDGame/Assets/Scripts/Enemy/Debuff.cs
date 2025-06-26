using System;
using UnityEngine;

public enum DebuffType
{
    Frozen,
    Burned
}

[Serializable]
public class Debuff
{
    public DebuffType type;
    public float duration;
    public float endTime;

    public Debuff(DebuffType type, float duration, float currentTime)
    {
        this.type = type;
        this.duration = duration;
        this.endTime = currentTime + duration;
    }

    public virtual void ApplyEffect(Enemy enemy) { }
    public virtual void OnEnd(Enemy enemy) { }
}

public class FrozenDebuff : Debuff
{
    private GameObject overlayInstance;

    public FrozenDebuff(float duration, float currentTime) : base(DebuffType.Frozen, duration, currentTime) { }

    public override void ApplyEffect(Enemy enemy)
    {
        // Change color to blue to indicate frozen state
        if (enemy.spriteRenderer != null)
        {
            enemy.spriteRenderer.color = Color.blue;
        }

        if (overlayInstance == null)
        {
            GameObject prefab = OverlayPoolManager.Instance.GetOverlayPrefab(DebuffType.Frozen);
            if (prefab != null)
                overlayInstance = GameObject.Instantiate(prefab, enemy.transform);
        }

        enemy.canAct = false;
    }

    public override void OnEnd(Enemy enemy)
    {
        if (overlayInstance != null)
        {
            Animator animator = overlayInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("End"); // Trigger de animación final
                if (enemy.spriteRenderer != null)
                {
                    enemy.spriteRenderer.color = Color.white; // Reset color to white
                }
                enemy.StartCoroutine(WaitAndReturnOverlay(overlayInstance, animator));
            } else
            {
                GameObject.Destroy(overlayInstance);
            }
            overlayInstance = null;
        }
        enemy.canAct = true;
    }

    private System.Collections.IEnumerator WaitAndReturnOverlay(GameObject overlay, Animator animator)
    {
        // Espera hasta que termine la animación "End"
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float waitTime = stateInfo.length;
        yield return new WaitForSeconds(waitTime);
        GameObject.Destroy(overlay);
        //OverlayPoolManager.Instance.ReturnToPool(overlay);
    }
}

public class BurnedDebuff : Debuff
{
    private GameObject overlayInstance;
    private float tickInterval = 1f;
    private float nextTick;

    public BurnedDebuff(float duration, float currentTime) : base(DebuffType.Burned, duration, currentTime)
    {
        nextTick = currentTime + tickInterval;
    }

    public override void ApplyEffect(Enemy enemy)
    {
        if (overlayInstance == null)
        {
            GameObject prefab = OverlayPoolManager.Instance.GetOverlayPrefab(DebuffType.Burned);
            if (prefab != null) {}
                //overlayInstance = OverlayPoolManager.Instance.GetFromPool(prefab, enemy.transform);
        }

        if (Time.time >= nextTick)
        {
            enemy.TakeDamage(1);
            nextTick = Time.time + tickInterval;
        }
    }

    public override void OnEnd(Enemy enemy)
    {
        if (overlayInstance != null)
        {
            //OverlayPoolManager.Instance.ReturnToPool(overlayInstance);
            overlayInstance = null;
        }
    }
}