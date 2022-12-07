using UnityEngine;

namespace WhacA_mole
{
    public class Burrow : MonoBehaviour
    {
        public int ID;

        public int InitBurrow()
        {
            GetComponentInChildren<Mole>().Init();
            return ID;
        }

    }
}
