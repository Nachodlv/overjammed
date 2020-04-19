using UnityEngine;

namespace Programmer
{
    public class ProgrammerAnimator: MonoBehaviour
    {
        [SerializeField] private GameObject armsUp;
        [SerializeField] private GameObject armsDown;

        public void PutArmsDown()
        {
            armsDown.SetActive(true);
            armsUp.SetActive(false);
        }

        public void PutArmsUp()
        {
            armsDown.SetActive(false);
            armsUp.SetActive(true);
        }
    }
}