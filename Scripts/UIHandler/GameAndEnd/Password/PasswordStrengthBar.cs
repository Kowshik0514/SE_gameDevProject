using UnityEngine;
using UnityEngine.UI;

public class PasswordStrengthChecker : MonoBehaviour
{
    [SerializeField] private InputField passwordInputField; // Reference to the InputField
    [SerializeField] private Image strengthBar;            // Reference to the UI Image (the bar)
    [SerializeField] private Gradient gradient;            // Gradient for color transitions
    [SerializeField] private CanvasGroup canvasGroup;      // For transparency control (optional)

    private void Start()
    {
        if (passwordInputField != null)
        {
            // Subscribe to the InputField's onValueChanged event
            passwordInputField.onValueChanged.AddListener(OnPasswordChanged);
        }

        if (canvasGroup == null)
        {
            canvasGroup = strengthBar.GetComponent<CanvasGroup>();
        }

        // Set initial transparency for the strength bar (optional)
        canvasGroup.alpha = 0.8f; // 80% opacity
    }

    // Called whenever the password is changed in the InputField
    private void OnPasswordChanged(string password)
    {
        int strength = EvaluatePasswordStrength(password); // Calculate password strength
        UpdateStrengthBar(strength);                      // Update the bar
    }

    // Simulate password strength calculation (you can replace this with your logic)
    private int EvaluatePasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password)) return 0;

        int strength = 0;
        int maxStrengthFromLength = 30;
        int maxLengthThreshold = 12;

        int lengthScore = Mathf.Min(password.Length, maxLengthThreshold) * 2;
        strength += Mathf.Min(lengthScore, maxStrengthFromLength);
        string specialChar = "[!@#$%^&*()\\,\\.?\":{}|<>]";

        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]")) strength += 20;
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) strength += 20;
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]")) strength += 20;
        if (System.Text.RegularExpressions.Regex.IsMatch(password, specialChar)) strength += 20;
        return Mathf.Clamp(strength, 0, 100); // Clamp the strength to a range of 0-100
    }

    // Updates the strength bar's fill and color based on the strength value
    private void UpdateStrengthBar(int strength)
    {
        float normalizedStrength = Mathf.Clamp01(strength / 100f); // Normalize the value between 0-1

        strengthBar.fillAmount = normalizedStrength;               // Update the fill amount
        strengthBar.color = gradient.Evaluate(normalizedStrength); // Update the color based on the gradient
    }
}
