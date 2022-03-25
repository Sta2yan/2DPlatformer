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
        StartCoroutine(PlayCollectAnimation());
    }

    private IEnumerator PlayCollectAnimation()
    {
        _animator.SetTrigger("Collect");

        yield return _delay;

        gameObject.SetActive(false);
    }
}
