using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BossDownslam : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnHeightOffset = 6f;

    [Header("Physics / Slam Settings")]
    public float bossMass = 50f;
    public float slamGravityScale = 8f;
    public float slamForce = 300f;
    public float windUpDelay = 0.8f;

    [Header("Impact Settings")]
    public float shakeIntensity = 0.4f;
    public float shakeDuration = 0.5f;
    public float shockwaveRadius = 4f;
    public float shockwaveForce = 600f;

    [Header("Visual Feedback")]
    public GameObject impactEffectPrefab;
    public Color windUpColor = new Color(1f, 0.3f, 0.1f);

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Color _originalColor;
    private bool _hasSlammed = false;
    private bool _isSlamming = false;
    private Camera _cam;

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
        Vector3 spawnPos = Vector3.zero;
        if (_cam != null)
            spawnPos = _cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        spawnPos.y += spawnHeightOffset;
        spawnPos.z = 0f;
        transform.position = spawnPos;

        StartCoroutine(SlamSequence());
    }

    private IEnumerator SlamSequence()
    {
        yield return StartCoroutine(WindUp());

        _isSlamming = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.gravityScale = slamGravityScale;
        _rb.AddForce(Vector2.down * slamForce, ForceMode2D.Impulse);

        if (_sr != null) _sr.color = _originalColor;
    }

    private IEnumerator WindUp()
    {
        float elapsed = 0f;
        float flashSpeed = 8f;

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
        Gizmos.color = new Color(1f, 0.3f, 0.1f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
    }
}
