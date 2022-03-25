using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CoinAnimation : MonoBehaviour
{
    private Animator _animator;
    private WaitForSeconds _delay = new WaitForSeconds(.7f);

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Collect()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        _animator.SetTrigger(CoinAnimController.Collect);

        yield return _delay;

        gameObject.SetActive(false);
    }
}
