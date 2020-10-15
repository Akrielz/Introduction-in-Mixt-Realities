using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChalkBehavior : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;

    private bool drawing;
    private GameObject chalk;

    public Color c1;
    public Color c2;
    public int lengthOfLineRenderer;

    private int frames;
    private int addKeyFrame;

    private LineRenderer lineRenderer;

    protected virtual void Start()
    {
        chalk = GameObject.Find("Chalk");
        drawing = false;
        lengthOfLineRenderer = 0;
        addKeyFrame = 3;
        frames = 0;
    }

    protected virtual void FixedUpdate()
    {
        frames++;

        if (!drawing)
            return;

        if (frames < addKeyFrame)
            return;

        frames = 0;

        //LineRenderer lineRenderer = GetComponent<LineRenderer>();
        
        lengthOfLineRenderer += 1;
        lineRenderer.positionCount = lengthOfLineRenderer;
        lineRenderer.SetPosition(lengthOfLineRenderer-1, chalk.transform.position);
        
    }

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        drawing = true;
        
        c1 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        c2 = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        lengthOfLineRenderer = 0;

        lineRenderer = new GameObject().AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = 0;

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        drawing = false;
    }
}
