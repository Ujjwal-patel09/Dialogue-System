using UnityEngine;
using UnityEngine.UI;
using LitJson;
using TMPro;

public class Options_Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TextMeshProUGUI DispalayText;
    [SerializeField] private GameObject[] Buttons;
    
    [HideInInspector]public bool inDialogue;
    [HideInInspector]public bool isAnimate = false;

    private JsonData dialogue;
    private JsonData Line;
    private int index;
    private string speaker;
    private JsonData CurrentLayer;

    private void Start() 
    { 
       DialogueBox.SetActive(false); 
       Dactivate_Buttons();
    }

    public void LoadDialogue(string path)// for Load dialogue from given path// this function is called from "NPC_Dialogue" script//
    {
        if(!inDialogue)
        {
           index = 0;// dialogue line number
           var JsonTextFile = Resources.Load<TextAsset>("Dialogue/"+ path);// call dialogue from Resources/Dialogue.. "this are predifine from assest folder" / path.
           dialogue = JsonMapper.ToObject(JsonTextFile.text);
           CurrentLayer = dialogue;
           inDialogue = true;
        }
    }

    public void  PrintLine()// for printing line from load data //this function is called from "NPC_Dialogue" script//
    {
        if(inDialogue)
        {
            Line = CurrentLayer[index];
            foreach (JsonData key in Line.Keys)
            {
                speaker = key.ToString();// for geting the speaker name from dialogue//
            }

            if(speaker == "End of Dialogue")// to end the conversation//
            {
                isAnimate = false;
                inDialogue = false;
                DispalayText.text = "";
                DialogueBox.SetActive(false);
                PlayerInteraction.instance.isInteract = false;
                return;
            }
            else if(speaker == "option")// for geting options//
            {
                isAnimate = false;
                PlayerInteraction.instance.isInteract = true;
                DispalayText.text = "";
                JsonData options = Line[0];
                for (int optionNumber = 0; optionNumber < options.Count; optionNumber++)// loop through all option and assign with buttons// 
                {
                   Activate_Buttons(Buttons[optionNumber],options[optionNumber]);
                }
            }
            else
            {
                DialogueBox.SetActive(true);// for display dialogue//
                DispalayText.text = speaker + ": " + Line[0].ToString();
                index++;
            }
        }  
    }

    

    // Buttons function //
    void OnClick_Button(JsonData choice)// this function is used for clicking buttons//
    {
        CurrentLayer = choice[0];
        index = 1;
        inDialogue = true;
        isAnimate = true;
        Dactivate_Buttons();
    }

    private void Dactivate_Buttons()// to dactivate butttons //
    {
        foreach (GameObject button in Buttons)
        {
            button.SetActive(false);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    private void Activate_Buttons(GameObject button , JsonData Choice)// to activate buutons//
    {
        button.SetActive(true);
        button.GetComponentInChildren<TextMeshProUGUI>().text = Choice[0][0].ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate{OnClick_Button(Choice);});// onclick button//
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
