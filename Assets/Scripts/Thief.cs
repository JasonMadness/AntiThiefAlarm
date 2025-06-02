using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class Thief : MonoBehaviour
{
    [SerializeField] private Chest _chest;
    [SerializeField] private Transform _exit;
    [SerializeField] private float _speed;

    private SpriteRenderer _spriteRenderer;
    private Transform _target;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _target = _chest.transform;
    }

    private void Update()
    {
        float targetPositionX = Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime);
        transform.position = new Vector2(targetPositionX, transform.position.y);

        if (transform.position.x == _chest.transform.position.x)
        {
            _target = _exit;
            _spriteRenderer.flipX = false;
            _chest.Open();
        }
    }
}
