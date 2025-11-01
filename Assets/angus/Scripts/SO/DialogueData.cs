using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Game/Dialogue")]
public class DialogueData : ScriptableObject
{
    [TextArea]
    public string[] DialogueText;
}
