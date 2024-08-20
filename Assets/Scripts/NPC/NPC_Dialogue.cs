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
    [SerializeField] private string path;
    [SerializeField] private Animator animator;
    
    [HideInInspector]public bool isStartConversation = false;
    
    private GameObject palyer;
    private bool isLook;
    private Conversation_Dialogue conversation_Dialogue = null;
    private Options_Dialogue options_Dialogue = null;
    private Bargaining_Dialogue bargaining_Dialogue = null;


    private void Start() 
    { 
        palyer = GameObject.FindGameObjectWithTag("Player");

        if(dialogue_Type == Dialogue_Type.conversation_Dialogues)
        {
            conversation_Dialogue = GameObject.FindGameObjectWithTag("Dialogue_Manager").GetComponent<Conversation_Dialogue>();
        }

        if(dialogue_Type == Dialogue_Type.Option_Dialogues)
        {
            options_Dialogue = GameObject.FindGameObjectWithTag("Dialogue_Manager").GetComponent<Options_Dialogue>();        
        }

        if(dialogue_Type == Dialogue_Type.Bargaining_Dialogues)
        {
            bargaining_Dialogue = GameObject.FindGameObjectWithTag("Dialogue_Manager").GetComponent<Bargaining_Dialogue>();
        }
    }

    private void Update() 
    {
        if(isLook)// for looking npc toward player//
        {
            transform.LookAt(palyer.transform.position);
        }else
        {
            transform.LookAt(null);
        }
        
        Talking_Animation();// For Animation

        
    }
     
    public void Interact()// from interface
    {
      StartConversation();
    }

    public void  StartConversation()
    {
        if(dialogue_Type == Dialogue_Type.conversation_Dialogues)
        {
            conversation_Dialogue.LoadDialogue(path);
            DisplayDialogue();
        }

        if(dialogue_Type == Dialogue_Type.Option_Dialogues)
        {
            options_Dialogue.LoadDialogue(path);
            DisplayDialogue();
        }
        
        if(dialogue_Type == Dialogue_Type.Bargaining_Dialogues)
        {
            bargaining_Dialogue.LoadDialogue(path);
            DisplayDialogue();
        }
    }

    private void DisplayDialogue()
    {   
        isLook = true;// for looking npc toward player//
        if(dialogue_Type == Dialogue_Type.conversation_Dialogues)
        {
           if(conversation_Dialogue.inDialogue)
           {
                conversation_Dialogue.isAnimate = true;
                conversation_Dialogue.PrintLine();
                StartCoroutine(PrintNextLine());
           }
           else
           {
                EndOfConversation();
           }
        }

        if(dialogue_Type == Dialogue_Type.Option_Dialogues)
        {   
            if(options_Dialogue.inDialogue)
            {
                options_Dialogue.isAnimate = true;
                options_Dialogue.PrintLine();
                StartCoroutine(PrintNextLine());
            }
            else
            {
                EndOfConversation();
            }
        }

        if(dialogue_Type == Dialogue_Type.Bargaining_Dialogues)
        {   
            if( bargaining_Dialogue.inDialogue)
            {
                bargaining_Dialogue.isAnimate = true;
                bargaining_Dialogue.PrintLine();
                StartCoroutine(PrintNextLine());
            }
            else
            {
                EndOfConversation();
            }
        }

    }

    IEnumerator PrintNextLine()
    {
      yield return new WaitForSeconds(2.5f);
      DisplayDialogue();  
    }

    void EndOfConversation()
    {
        StopAllCoroutines();
        isLook = false;
    }

    private void Talking_Animation()
    {
        if(dialogue_Type == Dialogue_Type.conversation_Dialogues)
        {
            if(conversation_Dialogue.isAnimate)
            {
               animator.SetBool("isTalking",true);
            }else
            {
                animator.SetBool("isTalking",false);
            }       
        }

        if(dialogue_Type == Dialogue_Type.Option_Dialogues)
        {
            if(options_Dialogue.isAnimate)
            {
               animator.SetBool("isTalking",true);
            }else
            {
                animator.SetBool("isTalking",false);
            }       
        }
        
        if(dialogue_Type == Dialogue_Type.Bargaining_Dialogues)
        {
            if(bargaining_Dialogue.isAnimate)
            {
               animator.SetBool("isTalking",true);
            }else
            {
                animator.SetBool("isTalking",false);
            }       
        }
    }
}
