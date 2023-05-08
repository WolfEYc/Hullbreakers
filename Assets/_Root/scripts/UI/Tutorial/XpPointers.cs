using Michsky.UI.MTP;
using UnityEngine;

namespace Hullbreakers
{
    public class XpPointers : MonoBehaviour
    {
        [SerializeField] StyleManager xpPickupNotif;

        void OnEnable()
        {
            Orb.OrbSpawned += XpOrbOnOrbSpawned;
        }

        void OnDisable()
        {
            Orb.OrbSpawned -= XpOrbOnOrbSpawned;
        }

        void XpOrbOnOrbSpawned()
        {
            Orb.OrbSpawned -= XpOrbOnOrbSpawned;
            xpPickupNotif.gameObject.SetActive(true);
        }

    }
}
