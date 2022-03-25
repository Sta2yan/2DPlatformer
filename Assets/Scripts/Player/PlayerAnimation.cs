using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis(GlobalVars.HorizontalAxis) > 0.01f || Input.GetAxis(GlobalVars.HorizontalAxis) < -0.01f)
            _animator.SetBool(PlayerAnimController.IsRun, true);
        else
            _animator.SetBool(PlayerAnimController.IsRun, false);
    }
}
