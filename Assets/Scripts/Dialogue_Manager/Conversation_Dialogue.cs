using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation_Dialogue : Dialogue_Base_Class
{
    protected override IEnumerator DisplayConversation(int dialogueIndex)
    {
      if (dialogueData != null && dialogueIndex >= 0)
        {
            DialogueBox.SetActive(true);
            isAnimate = true;
            isLook = true;
            
            foreach (Dialogue dialogue in dialogueData.dialogues)
            {
                foreach (string line in dialogue.lines)
                {
                    DisplayText.text = dialogue.character + ": " + line;
                    yield return new WaitForSeconds(2); // Wait for 1 seconds between lines
                }
            }

            EndOfConversation();// trigger when when there is no dialogue left
        }
        else
        {
            Debug.LogError("Dialogue data is null.");
        }
    }
}
