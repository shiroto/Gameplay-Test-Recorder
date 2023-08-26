using UnityEngine;
using UnityEngine.AI;

namespace TwoGuyGames.GTR.Samples
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RTSPlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;

        private IRTSInput input;
        private GameObject targetObject;

        [SerializeField]
        private GameObject targetPrefab;

        private void MoveToTarget(Vector2 posOnScreen)
        {
            Ray screenRay = Camera.main.ScreenPointToRay(posOnScreen);
            if (Physics.Raycast(screenRay, out RaycastHit hit, 75))
            {
                agent.destination = hit.point;
                if (targetObject)
                {
                    targetObject.transform.position = agent.destination;
                    targetObject.SetActive(true);
                }
            }
        }

        private void ProcessInput()
        {
            if (input.ShouldMove())
            {
                MoveToTarget(input.GetMovePosition());
            }
        }

        private void Start()
        {
            targetObject = Instantiate(targetPrefab);
            agent = GetComponent<NavMeshAgent>();
            input = GetComponent<IRTSInput>();
        }

        private void Update()
        {
            ProcessInput();
        }
    }
}