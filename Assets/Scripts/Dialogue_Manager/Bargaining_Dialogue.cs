using UnityEngine;
using UnityEngine.UI;
using LitJson;
using TMPro;

public class Bargaining_Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TextMeshProUGUI DispalayText;
    [SerializeField] private GameObject Button;
    [SerializeField] private GameObject slider_object;
    
    [HideInInspector]public bool inDialogue;
    [HideInInspector]public bool isAnimate;

    private JsonData dialogue;
    private JsonData Line;
    private int index;
    private string speaker;
    private JsonData CurrentLayer;

    Slider slider; 
    TextMeshProUGUI Slider_text;

    private void Start() 
    { 
       DialogueBox.SetActive(false); 
       Dactivate_Button_Slider();
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

    public void  PrintLine() // for printing line from load data //this function is called from "NPC_Dialogue" script//
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
                DispalayText.text = "ok ill give you";
                JsonData option = Line[0];
                Activate_Button_Slider(option[0],option[1]);// get all option in dialogue if have more add more//
                    
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
    void OnClick_Button(JsonData choice1,JsonData choice2)// this function is used for clicking buttons//
    {
        float maxammount;
        if(float.TryParse(choice1[0][0].ToString(), out maxammount))// getting the first line of dialogue which difines a number that is 
                                                                    //maxposiible number for bargaining and convert into float.//
        {
          if(slider.value >= maxammount/2)// selecting accept option//
          {
            inDialogue = true;
            CurrentLayer = choice1[0];
            index = 1;
            //PrintLine();
            Dactivate_Button_Slider();
          }
          else
          {
            inDialogue = true;
            CurrentLayer = choice2[0];// select dinied option//
            index = 0;
            //PrintLine();
            Dactivate_Button_Slider();
          }

        }
       
    }

    private void Dactivate_Button_Slider()// to dactivate buttton and slider//
    {
        Button.SetActive(false);
        slider_object.SetActive(false);
        Button.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void Activate_Button_Slider(JsonData choice1 ,JsonData choice2)// to activate buuton and slider//
    {
        Button.SetActive(true);
        Button.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";

        slider_object.SetActive(true);
        slider = slider_object.GetComponent<Slider>();
        Slider_text = slider.GetComponentInChildren<TextMeshProUGUI>();
        updateSilder(slider.value);
        slider.onValueChanged.AddListener(updateSilder);

        Button.GetComponent<Button>().onClick.AddListener(delegate{OnClick_Button(choice1,choice2);}); // onclick button//
        
       
    }
    private void updateSilder(float var)
    {
        Slider_text.text = slider.value.ToString();
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
