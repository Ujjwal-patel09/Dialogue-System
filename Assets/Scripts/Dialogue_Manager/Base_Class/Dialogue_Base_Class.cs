using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

// Abstract Base Class
public abstract class Dialogue_Base_Class : MonoBehaviour
{
    [SerializeField] protected GameObject DialogueBox;
    [SerializeField] protected TextMeshProUGUI DisplayText;
    [SerializeField] protected Button Skip_Button;

    protected DialogueData dialogueData;
    protected int currentDialogueIndex = 0;
    protected Coroutine currentCoroutine = null;

    //Animation & Lokking Variables// Used in npcDialogue class//
    [HideInInspector]public bool isAnimate;
    [HideInInspector]public bool isLook;
    
    protected virtual void Start()
    {
        DialogueBox.SetActive(false);
       // Skip_Button.onClick.AddListener(() => EndOfConversation());
    }

    public void LoadDialogue(string jsonFileName)// call from NPc_Dialogue script//
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Dialogue", jsonFileName);// path is streamingAssets/Dialogue/jsonFileName
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);// Convert data of json file to json string //
            dialogueData = JsonUtility.FromJson<DialogueData>(json);// convert Json string to DialogueData using jasonUtility//
            StartConversation(currentDialogueIndex);
        }
        else
        {
            Debug.LogError("Dialogue file not found at path: " + filePath);
        }
    }

    protected virtual void StartConversation(int dialogueIndex)
    {
        // Stop any currently running coroutine before starting a new one
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        
        // Start the new coroutine and store its reference
        currentCoroutine = StartCoroutine(DisplayConversation(dialogueIndex));
    }

    protected abstract IEnumerator DisplayConversation(int dialogueIndex);// Implement from drived classes -"ConversationDialogue","OptionalDialogue","BargainingDialogue" acrroding to there responses//

    protected virtual void EndOfConversation()
    {
       // Skip_Button.onClick.RemoveAllListeners();
        DialogueBox.SetActive(false);
        DisplayText.text = "";
        PlayerInteraction.instance.isInteract = false;
        isAnimate = false;
        isLook = false;
    }
}
