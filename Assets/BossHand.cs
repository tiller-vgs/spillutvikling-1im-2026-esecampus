using System.Collections;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public enum HandSide { Left, Right }

    [Header("Hand Setup")]
    public HandSide side = HandSide.Left;
    public Transform player;

    [Header("Tracking")]
    public float trackingSpeed = 3f;
    public float hoverHeight = 4f;
    public float horizontalOffset = 3f;
    public float trackingDuration = 3f;

    [Header("Slam Settings")]
    public float slamGravityScale = 10f;
    public float slamForce = 400f;
    public float windUpDelay = 0.6f;
    public float bossMass = 40f;

    [Header("Impact")]
    public float shakeIntensity = 0.3f;
    public float shakeDuration = 0.4f;
    public float shockwaveRadius = 3f;
    public float shockwaveForce = 500f;
    public GameObject impactEffectPrefab;

    [Header("Visual")]
    public Color windUpColor = new Color(1f, 0.2f, 0.05f);

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Color _originalColor;
    private Camera _cam;
    private bool _hasSlammed = false;
    private bool _isSlamming = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _cam = Camera.main;

        _rb.mass = bossMass;
        _rb.gravityScale = 0f;
        _rb.linearVelocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (_sr != null)
            _originalColor = _sr.color;
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) player = found.transform;
        }

        StartCoroutine(TrackThenSlam());
    }

    private IEnumerator TrackThenSlam()
    {
        float elapsed = 0f;

        while (elapsed < trackingDuration)
        {
            elapsed += Time.deltaTime;

            if (player != null)
            {
                float xOffset = side == HandSide.Left ? -horizontalOffset : horizontalOffset;
                Vector3 target = new Vector3(player.position.x + xOffset, player.position.y + hoverHeight, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, target, trackingSpeed * Time.deltaTime);
            }

            yield return null;
        }

        yield return StartCoroutine(WindUp());

        _isSlamming = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        _rb.gravityScale = slamGravityScale;
        _rb.AddForce(Vector2.down * slamForce, ForceMode2D.Impulse);

        if (_sr != null) _sr.color = _originalColor;
    }

    private IEnumerator WindUp()
    {
        float elapsed = 0f;
        float flashSpeed = 9f;

        while (elapsed < windUpDelay)
        {
            elapsed += Time.deltaTime;

            if (_sr != null)
            {
                float t = Mathf.PingPong(elapsed * flashSpeed, 1f);
                _sr.color = Color.Lerp(_originalColor, windUpColor, t);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_isSlamming || _hasSlammed) return;

        if (!col.gameObject.CompareTag("Ground") && col.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;

        _hasSlammed = true;
        _isSlamming = false;

        _rb.linearVelocity = Vector2.zero;
        _rb.gravityScale = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(OnImpact());
    }

    private IEnumerator OnImpact()
    {
        if (impactEffectPrefab != null)
            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);

        if (_cam != null)
            StartCoroutine(ScreenShake(_cam, shakeDuration, shakeIntensity));

        ApplyShockwave();

        if (_sr != null)
        {
            _sr.color = Color.white;
            yield return new WaitForSeconds(0.08f);
            _sr.color = _originalColor;
        }

        yield return null;
    }

    private void ApplyShockwave()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            Vector2 dir = (hit.transform.position - transform.position).normalized;
            dir = (dir + Vector2.up * 0.5f).normalized;
            rb.AddForce(dir * shockwaveForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator ScreenShake(Camera cam, float duration, float magnitude)
    {
        Vector3 originalPos = cam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float dampingFactor = 1f - (elapsed / duration);

            float offsetX = Random.Range(-1f, 1f) * magnitude * dampingFactor;
            float offsetY = Random.Range(-1f, 1f) * magnitude * dampingFactor;

            cam.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0f);
            yield return null;
        }

        cam.transform.localPosition = originalPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.2f, 0.05f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
    }
}
