using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class CanvasMethods : MonoBehaviour
    {
        public int buttonPressedCount;
        public int dropdownChangedCount;

        public void ButtonPressed()
        {
            Debug.LogWarning("Button Pressed");
            buttonPressedCount++;
        }

        public void DropdownChanged(int i)
        {
            Debug.LogWarning("Dropdown Changed");
            dropdownChangedCount++;
        }
    }
}