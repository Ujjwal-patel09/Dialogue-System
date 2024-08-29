using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueBarganning
{
    // NOTE :-
    // The name of the variables used in this class are case sensetive and Same as json file names & also in same structre as json 
    public int minAmount;         // Minimum amount the player can offer
    public int maxAmount;         // Maximum amount the player can offer
    public int responseLow_Index;  // Response if the player's offer is low
    public int DealDone_Index;   // Response if the player's offer is acceptable
}

[System.Serializable]
public class DialogueOption
{
    // NOTE :-
    // The name of the variables used in this class are case sensetive and Same as json file names & also in same structre as json 
    public string text; // The text displayed for the option
    public int nextDialogueIndex; // The index of the next dialogue block to jump to
}

[System.Serializable]
public class Dialogue
{
    // NOTE :-
    // The name of the variables used in this class are case sensetive and Same as json file names & also in same structre as json 
    // meening - first is charcter then lines and then options.
    public string character;
    public string[] lines;
    public DialogueOption[] options; // Options presented at the end of the dialogue
    public DialogueBarganning[] BarganningOptions;// Barganning Options presented at the end of the dialogue
    public int nextDialogueIndex; // The index of the next dialogue block to jump to
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogues;
}
