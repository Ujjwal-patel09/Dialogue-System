using System.Collections;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour,I_Interactable
{
    public enum Dialogue_Type
    {
        conversation_Dialogues,
        Option_Dialogues,
        Bargaining_Dialogues
    }
    
    [SerializeField] private Dialogue_Type dialogue_Type;
    [SerializeField] private string jsonFileName;// Write file name with .json extention//
    [SerializeField] private Animator animator;
    
    private GameObject player;
    private GameObject dialogueManager;
    private Dialogue_Base_Class CurrentDialogueType;

    private void Awake() 
    {
        dialogueManager = GameObject.FindGameObjectWithTag("Dialogue_Manager");

        switch (dialogue_Type)
        {
            case Dialogue_Type.conversation_Dialogues:
                CurrentDialogueType = dialogueManager.GetComponent<Conversation_Dialogue>();
                break;
            case Dialogue_Type.Option_Dialogues:
                CurrentDialogueType = dialogueManager.GetComponent<Options_Dialogue>();
                break;
            case Dialogue_Type.Bargaining_Dialogues:
                CurrentDialogueType = dialogueManager.GetComponent<Bargaining_Dialogue>();
                break;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() 
    {
        if(CurrentDialogueType.isLook)// for looking npc toward player//
        {
            transform.LookAt(player.transform.position);
        }else
        {
            transform.LookAt(null);
        }
        
        Talking_Animation();// For Animation
        
    }
     
    public void Interact()// interface function  within " I_Interactable" & call from player interaction script
    {
      StartConversation();
    }

    private void  StartConversation()
    {
        if (CurrentDialogueType != null)
        {
            CurrentDialogueType.LoadDialogue(jsonFileName);
        }
    }

    private void Talking_Animation()
    {
        if(CurrentDialogueType.isAnimate)
        {
           animator.SetBool("isTalking",true);
        }else
        {
            animator.SetBool("isTalking",false);
        }
    }
}
