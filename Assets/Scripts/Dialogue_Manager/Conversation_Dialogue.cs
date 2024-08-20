using UnityEngine;
using LitJson;
using TMPro;

public class Conversation_Dialogue : MonoBehaviour
{
    [SerializeField]private GameObject DialogueBox;
    [SerializeField]private TextMeshProUGUI DispalayText;

    [HideInInspector]public bool inDialogue = false;
    [HideInInspector]public bool isAnimate;

    private JsonData dialogue;
    private int index;
    private string speakerName;
    

    private void Start() 
    {
        DialogueBox.SetActive(false);
    }

    public void LoadDialogue(string path)// for Load dialogue from given path// this function is called from "NPC_Dialogue" script//
    {
        if(!inDialogue)
        {
           index = 0;
           var JsonTextFile = Resources.Load<TextAsset>("Dialogue/"+ path);// call dialogue from Resources/Dialogue.. "this are predifine from assest folder" / path.
           dialogue = JsonMapper.ToObject(JsonTextFile.text);
           inDialogue = true;
        }
    }

    public void  PrintLine()// for printing line from load data //this function is called from "NPC_Dialogue" script//
    {
        if(inDialogue)
        {
            JsonData Line = dialogue[index];

            foreach (JsonData key in Line.Keys)
            {
              speakerName = key.ToString();// for getting the speaker name from dialogue//
            }

            if(speakerName == "End of Dialogue")// to end the conversation//
            {
               isAnimate = false;
               inDialogue = false;
               DialogueBox.SetActive(false);
               DispalayText.text = "";
               PlayerInteraction.instance.isInteract = false;
               return;
            }
            
            DialogueBox.SetActive(true);
            
            DispalayText.text = speakerName + ": " + Line[0].ToString();
            index++;
        }   
    }

    public void Esc_Button()// used in esc button onclick to escape from conversation //   
    {
        isAnimate = false;
        inDialogue = false;
        DispalayText.text = "";
        DialogueBox.SetActive(false);
        PlayerInteraction.instance.isInteract = false;
    }
}
