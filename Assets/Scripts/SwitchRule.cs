using UnityEngine;

public class SwitchRule : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int rulesAnimationIndex = 1;
    private int revRulesAnimationIndex = 13;
    private bool isSwitching = true;
    //private int whichRule;

    public void OnSwitchRuleButtonClick()
    {
        //whichRule = 1;
        if (isSwitching)
        {
            if (revRulesAnimationIndex == 1)
                return;
            else
            {
                rulesAnimationIndex++;
                revRulesAnimationIndex--;
                switchAnimations();
            }
        }
        isSwitching = true;
        animator.SetBool("switch", isSwitching);
    }

    public void OnRevSwitchRuleButtonClick()
    {
        //whichRule = -1;
        if (!isSwitching)
        {
            if (rulesAnimationIndex == 2)
                return;
            else
            {
                rulesAnimationIndex--;
                revRulesAnimationIndex++;
                switchAnimations();
            }
        }
        isSwitching = false;
        animator.SetBool("switch", isSwitching);
    }

    private void switchAnimations()
    {
        animator.SetInteger("ruleNumber", rulesAnimationIndex);
        animator.SetInteger("revRuleNumber", revRulesAnimationIndex);
    }
}
