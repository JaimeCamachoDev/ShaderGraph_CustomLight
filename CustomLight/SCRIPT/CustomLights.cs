using UnityEngine;

[ExecuteInEditMode]
public class CustomLights : MonoBehaviour
{
    public Material Material_UNLITCustomLights;
    public bool lockLighting; // Si es true, se guarda la iluminación calculada

    private Color mainLightColor;
    private Vector3 mainLightDirection;
    private float mainLightIntensity;

    public Light additionalLight;
    private Color additionalLightColor;
    private float additionalLightIntensity;
    private Vector3 additionalLightDirection;

    private float distance;
    private float attenuation;

  


    private void Update()
    {
        if (Material_UNLITCustomLights == null)
        {
            Debug.LogError("No se ha asignado un material.");
            return;
        }

        Light mainLight = RenderSettings.sun;

        if (mainLight == null)
        {
            Debug.LogError("No se ha asignado una luz principal en Render Settings --> Sun.");
            return;
        }

        if (!lockLighting)
        {
            // **Capturar la luz principal**
            mainLightDirection = -mainLight.transform.forward;
            mainLightColor = mainLight.color;
            mainLightIntensity = mainLight.intensity;

            additionalLightColor = additionalLight.color;
            distance = Vector3.Distance(additionalLight.transform.position, transform.position);
            attenuation = Mathf.Clamp01(1.0f - (distance / additionalLight.range)); // Atenuación lineal
            additionalLightIntensity = additionalLight.intensity * (attenuation * attenuation); // Ajuste cuadrático
            additionalLightDirection = -(transform.position - additionalLight.transform.position).normalized;

            Material_UNLITCustomLights.SetFloat("_LockLight", 0.0f);
        }
        else
        {
            Material_UNLITCustomLights.SetFloat("_LockLight", 1.0f);
        }

        // **Guardar la luz principal**
        Material_UNLITCustomLights.SetFloat("_StoredIntensity", mainLightIntensity);
        Material_UNLITCustomLights.SetVector("_StoredLightDirection", mainLightDirection);
        Material_UNLITCustomLights.SetColor("_StoredLightColor", mainLightColor);

        // **Guardar la iluminación bakeada en el material**
        Material_UNLITCustomLights.SetFloat("_StoredAdditionalLightIntensity", additionalLightIntensity);
        Material_UNLITCustomLights.SetVector("_StoredAdditionalLightDirection", additionalLightDirection);
        Material_UNLITCustomLights.SetColor("_StoredAdditionalLightColor", additionalLightColor);
    }
}
