using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;
    private float maxMoveSpeed;
    private float trajectoryMaxRelativeHeight;
    private float dtttdp = 1f; //dttdp stands for Distance To Target To Destroy Projectile
    private AnimationCurve projectileSpeedAnimationCurve;
    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    
    private Vector3 trajectoryStartPoint;
    private Vector3 projectileMoveDir;


    private void Start()
    {
        trajectoryStartPoint = transform.position;
    }
    private void Update()
    {
        UpdateprojectilePosition();
        if(Vector3.Distance(transform.position, target.position) < dtttdp){
            Destroy(gameObject);
        }
    }
    private void UpdateprojectilePosition()
    {


        Vector3 trajectoryRange = target.position - trajectoryStartPoint;
        if (trajectoryRange.x < 0)
            // shooter located bhind the player
            moveSpeed = -moveSpeed;
        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalized * trajectoryRange.y;
        float nextPositionY = trajectoryStartPoint.y + nextPositionYNormalized * trajectoryMaxRelativeHeight + nextPositionYCorrectionAbsolute;
        
        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionXNormalized);
        projectileMoveDir = newPosition - transform.position;
        transform.position = newPosition;
    }
    private void CalculateNextProjectileSpeed(float nextPositionXNormalized)
    {
        float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
    }
    public void InitializeProjectile(Transform target, float maxMoveSpeed, float trajectoryMaxRelativeHeight)
    {
        this.target = target;
        this.maxMoveSpeed = moveSpeed;
        float xDistanceToTarget = target.position.x - transform.position.x;

        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxRelativeHeight;

        
    }
    public void InitializeAnimationCurves(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
        this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
        this.projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;

    }

    public Vector3 GetProjectileMoveDir()
    {
        return projectileMoveDir;
    }




}
