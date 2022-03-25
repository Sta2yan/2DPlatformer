using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _stopper;

    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _sleep = new WaitForSeconds(2);
    private Target[] _targets;
    private int _targetIndex;

    public bool IsMoving { get; private set; } = true;

    private void Update()
    {
        if (IsMoving)
            Move();
    }

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stopper.transform.position = new Vector3(_stopper.transform.position.x, transform.position.y, _stopper.transform.position.z);
        _targets = _stopper.GetComponentsInChildren<Target>();
    }

    public void StartStayTime()
    {
        StartCoroutine(StayTime());
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targets[_targetIndex].Coordinates.position, _speed * Time.deltaTime);
    }

    private void Stay()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
        _targetIndex = _targetIndex == 0 ? 1 : 0;
    }

    private IEnumerator StayTime()
    {
        IsMoving = false;
        yield return _sleep;

        Stay();
        IsMoving = true;
    }
}
