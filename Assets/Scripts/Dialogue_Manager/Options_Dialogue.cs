using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options_Dialogue :  Dialogue_Base_Class
{
    [SerializeField] private GameObject[] OptionButtons;

    protected override void Start()
    {
        base.Start();
        Dactivate_Buttons();
    }

    protected override IEnumerator DisplayConversation(int dialogueIndex)
    {
       if (dialogueData != null && dialogueIndex >= 0 && dialogueIndex < dialogueData.dialogues.Length)
        {
            DialogueBox.SetActive(true);
            isAnimate = true;
            isLook = true;

            Dialogue dialogue = dialogueData.dialogues[dialogueIndex];// coverting dialogue from dialogueData.dialogues arreys according to index//

            // Display each line of dialogue
            foreach (string line in dialogue.lines)
            {
                DisplayText.text = dialogue.character + ": " + line;
                yield return new WaitForSeconds(2); // Wait for 2 seconds between lines
            }

            // Display options if any
            if (dialogue.options != null && dialogue.options.Length > 0)
            {
                isAnimate = false;
                for (int optionNumber = 0; optionNumber < dialogue.options.Length; optionNumber++)// loop through all option and assign with buttons// 
                {
                   Activate_Buttons(OptionButtons[optionNumber],dialogue.options[optionNumber]);
                }
            }
            else
            {
                StartConversation(dialogue.nextDialogueIndex); // The nextDialogueIndex used here is under "**Dialogue class**" and
                                                               // in line 99 nextDialogueIndex variable is under  "**DialogueOption class**"//
            }
        }
        else
        {
            EndOfConversation(); // End the dialogue if dialogue.nextDialogueIndex is -1 in line 82// 
        }
    }

    private void  Activate_Buttons(GameObject button , DialogueOption dialogueOption)
    {
        button.SetActive(true);
        button.GetComponentInChildren<TextMeshProUGUI>().text = dialogueOption.text;
        button.GetComponent<Button>().onClick.AddListener(()=> OnOptionSelected(dialogueOption.nextDialogueIndex));// onclick button //
    }

    private void OnOptionSelected(int nextDialogueIndex)
    {
        Dactivate_Buttons();
        StartConversation(nextDialogueIndex);
    }

    private void Dactivate_Buttons()
    {
        foreach (GameObject button in OptionButtons)
        {
            button.SetActive(false);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    protected override void EndOfConversation()
    {
        base.EndOfConversation();
        Dactivate_Buttons();
    }

}
