using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bargaining_Dialogue : Dialogue_Base_Class
{
    [SerializeField] private GameObject SubmitButton;// Button Gameobject to submit the offer
    [SerializeField] private Slider offerSlider;      // Slider for player's offer
    [SerializeField] public TextMeshProUGUI SliderText; // Text to show the player's current offer

    private Button _submitButton; // Button Component Form Sbmit Button Gameobject//
    private TextMeshProUGUI _SubmitButtonText;// Button Text //

    private void Awake()
    {
        _submitButton = SubmitButton.GetComponent<Button>();
        _SubmitButtonText = SubmitButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        SetSliderActive(false);
    }

    protected override IEnumerator DisplayConversation(int dialogueIndex)
    {
        if (dialogueData != null && dialogueIndex >= 0 /* Use this parameter to end conversation by -1 in dialogue jsonfile */ && dialogueIndex < dialogueData.dialogues.Length)
        {
            DialogueBox.SetActive(true);
            isAnimate = true;
            isLook = true;
            
            Dialogue dialogue = dialogueData.dialogues[dialogueIndex];

            // Display each line of dialogue
            foreach (string line in dialogue.lines)
            {
                DisplayText.text = dialogue.character + ": " + line;
                yield return new WaitForSeconds(3); // Wait for 2 seconds between lines
            }

            // Display slider if options exist
            if (dialogue.BarganningOptions != null && dialogue.BarganningOptions.Length > 0)
            {
                DialogueBarganning currentOption;
                currentOption = dialogue.BarganningOptions[0]; // Assuming one option here for simplicity
                SetSliderActive(true);
                isAnimate = false;
                DisplayText.text = "You : ok Ill give you";
                offerSlider.value = (currentOption.minAmount + currentOption.maxAmount) / 2;// Set the slider to the midpoint
                _submitButton.onClick.AddListener(() => OnOfferMade(currentOption));// onClick button//
            }
            else
            {
                StartConversation(dialogue.nextDialogueIndex); // The nextDialogueIndex used here is under "**Dialogue class**".
            }
        }
        else
        {
           EndOfConversation();
        }
    }

    private void OnOfferMade(DialogueBarganning option)
    {
        SetSliderActive(false);
        int playerOffer = Mathf.RoundToInt(offerSlider.value);// To make offer it int value only //

        if (playerOffer < option.minAmount)
        {
            StartConversation(option.responseLow_Index);// relpy if player offers in less than min ammount//  
        }
        else if (playerOffer >= option.minAmount && playerOffer <= option.maxAmount)
        {
            StartConversation(option.DealDone_Index);
            return; // Early return as the offer was accepted
        }
    }

    private void SetSliderActive(bool isActive)
    {
        offerSlider.gameObject.SetActive(isActive);
        SliderText.gameObject.SetActive(isActive);
        SubmitButton.gameObject.SetActive(isActive);
        _SubmitButtonText.text = "Continue";
        _submitButton.onClick.RemoveAllListeners();
    }

    protected override void EndOfConversation()
    {
        base.EndOfConversation();
        SetSliderActive(false);
    }

    //****** slider event function ******* //
    public void OnSliderValueChanged()// assingn the function in  slider onvalue change event //
    {
        SliderText.text = offerSlider.value.ToString() + "RS";
    }
}
