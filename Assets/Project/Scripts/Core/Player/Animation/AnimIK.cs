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
        public TwoBoneIKConstraint LeftHandConstraint => leftHandConstraint;
        public TwoBoneIKConstraint RightHandConstraint => rightHandConstraint;

        public void SetIKTargets(Transform rightHand, Transform rightElbow, Transform leftHand, Transform leftElbow)
        {
            rightHandConstraint.data.target = rightHand;
            rightHandConstraint.data.hint = rightElbow;
            leftHandConstraint.data.target = leftHand;
            leftHandConstraint.data.hint = leftElbow;
            rigBuilder.Build();
        }

        public void SetActive(bool active)
        {
            if (rig != null) rig.weight = active ? 1f : 0f;
        }

        public void SetLeftHandActive(bool active)
        {
            if (leftHandConstraint != null)
                leftHandConstraint.weight = active ? 1f : 0f;
        }
    }
}