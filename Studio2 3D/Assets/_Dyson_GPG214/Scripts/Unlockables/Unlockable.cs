using UnityEngine;


namespace SAE.GPG214.Dyson.Unlockable
{
    public class Unlockable : MonoBehaviour
    {
        public UnlockableData unlock;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                unlock.UnlockPower(UnlockableData.UnlockablePower.LevelTwoSword);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                unlock.UnlockPower(UnlockableData.UnlockablePower.Fire);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                unlock.UnlockPower(UnlockableData.UnlockablePower.Ultimate);
            }
        }
    }
}