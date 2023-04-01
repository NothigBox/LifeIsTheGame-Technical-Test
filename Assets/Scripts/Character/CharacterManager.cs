using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static string lastAnim;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if(lastAnim != default) ChangeAnimation(lastAnim);
    }

    public void ChangeAnimation(string newAnim)
    {
        animator.SetTrigger(newAnim);

        animator.transform.position = Vector3.zero;
        animator.transform.rotation = Quaternion.Euler(Vector3.zero);

        lastAnim = newAnim;
    }
}
