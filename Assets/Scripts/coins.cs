using UnityEngine;
using TMPro;

public class CapsuleCounter : MonoBehaviour
{
    public TextMeshProUGUI capsuleCountText;
    private int capsuleCount = 0;

    void Start()
    {
        UpdateCapsuleCountText();
    }

    // Function to add destroyed capsule
    public void AddDestroyedCapsule()
    {
        capsuleCount++;
        UpdateCapsuleCountText();
    }

    // Function to update the UI Text with the current capsule count
    private void UpdateCapsuleCountText()
    {
        if (capsuleCountText != null)
        {
            capsuleCountText.text = "Coins: " + capsuleCount.ToString();
        }
        else
        {
            Debug.LogWarning("Capsule Count Text not assigned.");
        }
    }
}