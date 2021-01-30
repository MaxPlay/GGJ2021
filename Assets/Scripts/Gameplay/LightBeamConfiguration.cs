using UnityEngine;

namespace GGJ.Gameplay
{
    [CreateAssetMenu(menuName = "GGJ/Light Beam Configuration")]
    public class LightBeamConfiguration : ScriptableObject
    {
        public int StartWidth;

        public int EndWidth;

        public int Distance;

        public int Length;
    }
}