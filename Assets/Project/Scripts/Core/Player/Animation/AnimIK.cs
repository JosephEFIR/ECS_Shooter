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

        public void SetIKTargets(Transform leftHand, Transform rightHand, Transform leftHint, Transform rightHint)
        {
            leftHandConstraint.data.target = leftHand;
            rightHandConstraint.data.target = rightHand;
            rightHandConstraint.data.hint = rightHint;
            leftHandConstraint.data.hint = leftHint;
            rigBuilder.Build();
        }
    }
}