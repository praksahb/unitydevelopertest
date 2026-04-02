using UnityEngine;

namespace DevTest.Gravity
{
    public class GravityModel
    {
        public Vector3 TargetRotationAxis { get; set; }
        public float TargetRotationAngle { get; set; }
        public bool IsSelecting { get; set; }
    }
}
