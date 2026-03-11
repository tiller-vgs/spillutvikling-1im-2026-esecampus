using System.Collections;
using UnityEngine;

public class BossFlyingMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;
    public float flyHeight = 5f;
    public float horizontalRange = 8f;

    [Header("u pathism")]
    [Range(0f, 1f)]
    public float straightPortion = 0.25f;
    public AnimationCurve verticalCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector3 _startPos;
    private float _progress = 0f;
    private float _pathLength;

    private void Start()
    {
        _startPos = transform.position;
        _pathLength = horizontalRange * 2f;
    }

    private void Update()
    {
        _progress += (speed / _pathLength) * Time.deltaTime;
        _progress = Mathf.Clamp01(_progress);

        transform.position = EvaluatePath(_progress);

        if (_progress >= 1f)
            OnPathComplete();
    }

    private Vector3 EvaluatePath(float t)
    {
        float x = Mathf.Lerp(-horizontalRange, horizontalRange, t);

        float dropT;
        if (t < straightPortion)
        {
            dropT = 0f;
        }
        else
        {
            dropT = (t - straightPortion) / (1f - straightPortion);
        }

        float uShape = Mathf.Sin(dropT * Mathf.PI);
        float curvedDrop = verticalCurve.Evaluate(dropT);
        float y = _startPos.y + flyHeight - (curvedDrop * flyHeight) - (uShape * flyHeight * 0.3f);

        return new Vector3(_startPos.x + x, y, _startPos.z);
    }

    private void OnPathComplete()
    {
        enabled = false;

        BossDownslam slam = GetComponent<BossDownslam>();
        if (slam != null)
            slam.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.cyan;
        Vector3 prev = EvaluatePath(0f);
        for (int i = 1; i <= 40; i++)
        {
            Vector3 next = EvaluatePath(i / 40f);
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
}
