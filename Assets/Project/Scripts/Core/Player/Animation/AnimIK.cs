using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Project.Scripts.Animation
{
    public class AnimIK : MonoBehaviour
    {
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private TwoBoneIKConstraint leftHandConstraint;
        [SerializeField] private TwoBoneIKConstraint rightHandConstraint;

        private bool _targetsInitialized;

        public TwoBoneIKConstraint LeftHandConstraint => leftHandConstraint;
        public TwoBoneIKConstraint RightHandConstraint => rightHandConstraint;

        public void SetIKTargets(Transform rightHand, Transform rightElbow, Transform leftHand, Transform leftElbow)
        {
            rightHandConstraint.data.target = rightHand;
            rightHandConstraint.data.hint = rightElbow;
            leftHandConstraint.data.target = leftHand;
            leftHandConstraint.data.hint = leftElbow;

            if (!_targetsInitialized)
            {
                _targetsInitialized = true;
                rigBuilder.Build();
            }
        }
    }
}