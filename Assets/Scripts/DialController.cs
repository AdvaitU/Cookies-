// Summary: Controls the dial showing Credit Score in the UI.

using UnityEngine;
using UnityEngine.UI;

public class DialController : MonoBehaviour
{
    [Header("References")]
    public RectTransform needleTransform;

    [Header("Credit Score Settings")]
    public float minCreditScore = 300f;
    public float maxCreditScore = 850f; // Capped at 70% of 850

    [Header("Dial Angle Settings")]
    public float minAngle = -90f; // Left end
    public float maxAngle = 90f;  // Right end

    // Called in CreditScoreGenerator after generating the score. Argument passed as value is mapped to min and max angle in z-axis rotation
    public void SetDialValue(float creditScore)
    {
        float t = Mathf.InverseLerp(minCreditScore, maxCreditScore, creditScore);
        float angle = Mathf.Lerp(minAngle, maxAngle, t);
        needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

