using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast can ship move one the screen based upon player input")]
    [SerializeField] float movementSpeed;
    [SerializeField] float positionPitchFactor;
    [SerializeField] float controlPitchFactor;
    [SerializeField] float yawFactor;

    [Header("Min/Max Range for Ship on the screen")]
    [Tooltip("How far can ship go to the left side of the screen")]
    [SerializeField] float minX;
    [Tooltip("How far can ship go to the right side of the screen")]
    [SerializeField] float maxX;
    [Tooltip("How far can ship go to the bottom side of the screen")]
    [SerializeField] float minY;
    [Tooltip("How far can ship go to the upper side of the screen")]
    [SerializeField] float maxY;

    [Header("Particle of lasers")] 
    [SerializeField] private ParticleSystem[] lasersParticles;

    private float horizontalThrow, verticalThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");


        Vector3 moveTo = new Vector3(
                Mathf.Clamp(transform.localPosition.x + horizontalThrow * Time.deltaTime * movementSpeed, minX, maxX),
                Mathf.Clamp(transform.localPosition.y + verticalThrow * Time.deltaTime * movementSpeed, minY, maxY),
                transform.localPosition.z
            );

        transform.localPosition = moveTo;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * yawFactor;

        float roll = horizontalThrow * controlPitchFactor + transform.localPosition.x / 2;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetLasersActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (ParticleSystem item in lasersParticles)
        {
            var main = item.main;
            main.loop = isActive;
            if (isActive)
            {
                item.Play();
            }
        }
    }
}
