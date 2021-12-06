using System;
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

        _actionBasedController.selectActionValue.action.performed += GoToTarget;
        _actionBasedController.selectActionValue.action.canceled += GoToTarget;
    }

   private void Start()
   {
       defaultPos = AnimatedTransform.localPosition;
   }

   //Change this to go to a target. Lerp it
   //Target should have certain positions:
   //Rest position = no buttons pressed
   //Default = button pressed
   //Dynamic target = button pressed but an interactable is near
   
   //Change the target's code in its own script, not here. So only use the GoToTarget function
   //Maybe assign a listener property that notifies this script when the target has changed.
   
    private void Animate(float contextValue)
    {
        Vector3 position = AnimatedTransform.localPosition;
        Vector3 newPos = new(position.x, position.y,defaultPos.z * contextValue);
        position = newPos;
        AnimatedTransform.localPosition = position;
    }

    private float GetValue(InputAction.CallbackContext callbackContext)
    {
        float triggerImpulse = callbackContext.ReadValue<float>();

        return _animationCurve.Evaluate(triggerImpulse);
    }

    void GoToTarget(InputAction.CallbackContext callbackContext)
    {
        AnimatedTransform.localPosition = targetTransform.localPosition;
    }


    private void Update()
    {
        //Animate(GetValue(Value));
    }
}
