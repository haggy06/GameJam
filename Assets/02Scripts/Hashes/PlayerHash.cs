using UnityEngine;

internal class PlayerHash
{
    private static int isMove = Animator.StringToHash("IsMove");
    public static int IsMove => isMove;


    private static int isGround = Animator.StringToHash("IsGround");
    public static int IsGround => isGround;


    private static int jump = Animator.StringToHash("Jump");
    public static int Jump => jump;

    /*
    private static int selected = Animator.StringToHash("Selected");
    public static int Selected => selected;


    private static int disabled = Animator.StringToHash("Disabled");
    public static int Disabled => disabled;
    */
}
