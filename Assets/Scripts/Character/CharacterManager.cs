using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private CharacterFPVMouse characterFPVMouse;
    [SerializeField] private bool useFPVLocalRotation;

    private static string lastAnim;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if(characterFPVMouse != null) characterFPVMouse.useLocalRotation = useFPVLocalRotation;

        if(lastAnim != default) ChangeAnimation(lastAnim);
    }

    public void ChangeAnimation(string newAnim)
    {
        animator.SetTrigger(newAnim);

        animator.transform.position = Vector3.zero;
        animator.transform.rotation = Quaternion.Euler(Vector3.zero);

        lastAnim = newAnim;
    }

    private void OnValidate()
    {
        if (characterFPVMouse != null) characterFPVMouse.useLocalRotation = useFPVLocalRotation;
    }
}
