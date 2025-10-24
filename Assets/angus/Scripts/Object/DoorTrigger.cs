using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorTrigger : InteractableItem
{
    [Header("Door Setup")]
    public bool locked;
    public Transform HingeTransform;
    public float openAngle = 90f;
    public float turnSpeed = 180f;

    [SerializeField] private Quaternion originRotation;
    [SerializeField] private bool opened;

    private Coroutine rotateCR;

    void Awake()
    {
        originRotation = HingeTransform.rotation;
    }

    public override void Interact(PlayerInteraction player)
    {
        if (locked)
        {
            HintManager.Instance.ShowHint(hint);
        }
        else
        {
            ToggleDoor(player.transform);
        }
    }

    public void ToggleDoor(Transform player)
    {
        opened = !opened;
        if (rotateCR != null) StopCoroutine(rotateCR);
        rotateCR = StartCoroutine(DoorRotate(opened, player));
    }

    private IEnumerator DoorRotate(bool toOpen, Transform player)
    {
        Quaternion target = originRotation;

        if (toOpen)
        {
            Vector3 toPlayer = player.position - HingeTransform.position;
            float frontSign = Mathf.Sign(Vector3.Dot(HingeTransform.forward, toPlayer));


            float angle = frontSign * openAngle;


            target = originRotation * Quaternion.AngleAxis(angle, HingeTransform.up);
        }
        GetComponent<BoxCollider>().enabled = false;

        
        while (Quaternion.Angle(HingeTransform.rotation, target) > 0.1f)
        {
            HingeTransform.rotation = Quaternion.RotateTowards(
                HingeTransform.rotation,
                target,
                turnSpeed * Time.deltaTime
            );
            yield return null;
        }

        GetComponent<BoxCollider>().enabled = true;
        HingeTransform.rotation = target; // 對齊終點避免殘差
        rotateCR = null;
    }
}
