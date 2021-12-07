using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimations : MonoBehaviour
{
    [SerializeField] private ActionBasedController _actionBasedController;

    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Transform AnimatedTransform;
    [SerializeField] private Vector3 defaultPos;
    [field: SerializeField] public InputAction.CallbackContext Value { get; set; }
    [SerializeField] private Transform targetTransform;

   private void Awake()
    {
        _actionBasedController ??= GetComponent<ActionBasedController>();
        //_actionBasedController.selectActionValue.action.performed += context => Value = context;
        //_actionBasedController.selectActionValue.action.canceled += context => Value = context;

       // _actionBasedController.selectActionValue.action.performed += GoToTarget;
       // _actionBasedController.selectActionValue.action.canceled += ResetTarget;

        //defaultPos = AnimatedTransform.localPosition;
    }

   private void Start()
   {
       defaultPos = AnimatedTransform.localPosition;
   }

   //Change the target's code in its own script, not here. So only use the GoToTarget function
   //Maybe assign a listener property that notifies this script when the target has changed.
   
   //The triggerimpulse float value = our chain IK constraint

   void GoToTarget(InputAction.CallbackContext callbackContext)
    {
        float triggerImpulse = callbackContext.ReadValue<float>();
        float lol = _animationCurve.Evaluate(triggerImpulse);// * Time.deltaTime;
        //AnimatedTransform.localPosition = Vector3.MoveTowards(AnimatedTransform.localPosition, targetTransform.localPosition, lol);
        AnimatedTransform.localPosition = targetTransform.localPosition;
        //AnimatedTransform.localPosition += lol + targetTransform.localPosition;
    }

   void ResetTarget(InputAction.CallbackContext callbackContext)
   {
       AnimatedTransform.localPosition = defaultPos;
   }

   void Animate(float selectVal)
   {
       float triggerImpulse = selectVal;
       float lol = _animationCurve.Evaluate(triggerImpulse);// * Time.deltaTime;
       //AnimatedTransform.localPosition = Vector3.MoveTowards(defaultPos, targetTransform.localPosition, _animationCurve.Evaluate(triggerImpulse));
       
       AnimatedTransform.localPosition = Vector3.Lerp(defaultPos, targetTransform.localPosition, _animationCurve.Evaluate(triggerImpulse));
   }

   
   [SerializeField] 
   [Range(0, 1)]
   private float SelectVal;
   
#if UNITY_EDITOR
    private void Update()
    {
        float selectval = SelectVal;
        Animate(selectval);
        //GoToTarget(Value);
    }
#else
    private void Update()
    {
        float selectval = _actionBasedController.selectActionValue.action.ReadValue<float>();
        Animate(selectval);
        //GoToTarget(Value);
    }
#endif
}
