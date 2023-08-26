using UnityEngine;

namespace TwoGuyGames.GTR.Samples
{
    public class FrontThrusterVisuals : MonoBehaviour
    {
        public void OnMoveInput(Vector3 input)
        {
            if (input.x < 0)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}