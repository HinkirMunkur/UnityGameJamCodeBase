using EasyButtons;
using UnityEngine;

public class PlayerAnimationControllerTest : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private EPlayerAnimation ePlayerAnimation;

    [Button]
    private void PlayPlayerAnimation()
    {
        playerAnimationController.PlayAnimation(ePlayerAnimation, OnAnimationFinished: () =>
        {
            Debug.Log(ePlayerAnimation.ToString() + " FINISHED");
        });
        
        Debug.Log(ePlayerAnimation.ToString() + " STARTED");
    }
}
