using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
  public FPS_Player_Movement fPS_Player_Movement;
  public Animator playerAnimator;
    
  void Update()
  {
    transform.localPosition = new Vector3(0,0,0);

    // for walking animation //
    if(fPS_Player_Movement.Walking)
    {
        playerAnimator.SetBool("isWalking",true);
    }else
    {
        playerAnimator.SetBool("isWalking",false);
    }

    // for running animation //
    if(fPS_Player_Movement.Running)
    {
        playerAnimator.SetBool("isRunning",true);    
    }else
    {
        playerAnimator.SetBool("isRunning",false);
    }

    /*   
    // for jumping animation //
    if(fPS_Player_Movement.Jump)
    {
        playerAnimator.SetBool("isJump",true);
    }else
    {
        playerAnimator.SetBool("isJump",false);
    }*/
        
  }
}
