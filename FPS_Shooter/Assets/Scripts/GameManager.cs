using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Damage Indicator")]
    public Image damageIndicator;
    public float displayDuration = 0.5f;

    [Header("Camera Settings")]
    public float cameraTiltAngle = 15f;
    public float tiltDuration = 0.5f;

    void Start()
    {
        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);
        }
    }

    public IEnumerator ShowDamageIndicator()
    {
        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(displayDuration);
            damageIndicator.gameObject.SetActive(false);
        }
    }

    public IEnumerator TiltCamera(Transform cameraTransform)
    {
        Vector3 currentRotation = cameraTransform.localEulerAngles;
        cameraTransform.localEulerAngles = new Vector3(cameraTiltAngle, currentRotation.y, currentRotation.z);
        yield return null;
    }
}