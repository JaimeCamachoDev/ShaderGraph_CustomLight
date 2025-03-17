using UnityEngine;
using UnityEditor;
[ExecuteInEditMode] // Permite que el script funcione en el editor
public class CustomDirectionalLight : MonoBehaviour
{
    public Material Material_CustomDirectionalLight;
    public bool lockLight; // Ahora es un bool para mayor claridad.
    private Color color;
    private Vector3 lightDirection;
    private float intensity;


    

    private void Update()
    {
        if (Material_CustomDirectionalLight == null) { 
        Debug.LogError("No se ha asignado un material.");
        return;
        }
        
        Light mainLight = RenderSettings.sun;

        if (mainLight == null)
        {
            Debug.LogError("No se ha asignado una luz principal en render Settings --> Sun.");
            return;
        }
       
        if (!lockLight) // Solo actualizamos si LockLight está en FALSE
        {
            lightDirection = -mainLight.transform.forward;
            color = mainLight.color;
            intensity = mainLight.intensity;
            // material.SetVector("_StoredLightDirection", -mainLight.transform.forward);
            // material.SetColor("_StoredLightColor", mainLight.color);
            Material_CustomDirectionalLight.SetFloat("_StoredIntensity", intensity);
            Material_CustomDirectionalLight.SetVector("_StoredLightDirection", lightDirection);
            Material_CustomDirectionalLight.SetColor("_StoredLightColor", color);

        }
        else {

            Material_CustomDirectionalLight.SetFloat("_StoredIntensity", intensity);
            Material_CustomDirectionalLight.SetVector("_StoredLightDirection", lightDirection);
            Material_CustomDirectionalLight.SetColor("_StoredLightColor", color);



        }



    }
}
