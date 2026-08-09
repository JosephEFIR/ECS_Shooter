using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project.Scripts.Animation
{
    public class AnimIK : MonoBehaviour
    {
        [SerializeField] private Rig rig; 
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private TwoBoneIKConstraint leftConstraint;
        [SerializeField] private TwoBoneIKConstraint rightConstraint;
        public Rig Rig => rig;

        [Range(0, 1)] [SerializeField] private float handIKAmount = 1f;
        [Range(0, 1)] [SerializeField] private float elbowIKAmount = 1F;

        public void SetIKTargets(Transform leftHand, Transform rightHand)
        {
            leftConstraint.data.target = leftHand;
            rightConstraint.data.target = rightHand;
            rigBuilder.Build();
        }
    }
}