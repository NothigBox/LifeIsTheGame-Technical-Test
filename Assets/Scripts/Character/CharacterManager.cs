using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Tooltip("If true, uses the local rotation in the transform of the First Person View script.")]
    [SerializeField] private bool useFPVLocalRotation;

    private static string lastAnim;
    private CharacterFPVMouse characterFPVMouse;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterFPVMouse = GetComponentInChildren<CharacterFPVMouse>();

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
