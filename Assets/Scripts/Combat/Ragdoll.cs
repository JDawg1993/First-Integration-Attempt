using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] allColliders;
    private Rigidbody[] allRB;


    private void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRB = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);

    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allColliders)
        {
            if(collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;
            }
        }
        foreach (Rigidbody rb in allRB)
        {
            if(rb.gameObject.CompareTag("Ragdoll"))
            {
                rb.isKinematic = !isRagdoll;
                rb.useGravity = isRagdoll;
            }
        }
        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }

}
