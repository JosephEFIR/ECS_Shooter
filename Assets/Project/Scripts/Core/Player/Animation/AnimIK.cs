using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project.Scripts.Animation
{
    public class AnimIK : MonoBehaviour
    {
        [SerializeField] private Rig rig; 
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private TwoBoneIKConstraint leftHandConstraint;
        [SerializeField] private TwoBoneIKConstraint rightHandConstraint;
        public Rig Rig => rig;

        public void SetIKTargets(Transform leftHand, Transform rightHand)
        {
            leftHandConstraint.data.target = leftHand;
            rightHandConstraint.data.target = rightHand;
            rigBuilder.Build();
        }
    }
}