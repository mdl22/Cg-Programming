Shader "Cg/WholeBrain/Two-sided normal mapping with discard"
{
    Properties
    {
        _MainTex ("Texture For Diffuse Material Color", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Color ("Diffuse Material Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Material Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Float) = 10
        _BackColor ("Back Diffuse Material Color", Color) = (1,1,1,1)
        _BackSpecColor ("Back Specular Material Color", Color) = (1,1,1,1)
        _BackShininess ("Back Shininess", Float) = 10
    }

    CGINCLUDE // common code for all passes of all subshaders

        #include "UnityCG.cginc"
        uniform float4 _LightColor0;
        // color of light source (from "Lighting.cginc")

        // User-specified properties
        uniform sampler2D _MainTex;
        uniform sampler2D _BumpMap;
        uniform float4 _BumpMap_ST;
        uniform float4 _Color;
        uniform float4 _SpecColor;
        uniform float _Shininess;
        uniform float _CutterRadius;
        uniform float4x4 _InverseModelMatrix;

        struct vertexInput
        {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
        };
        struct vertexOutput
        {
            float4 pos : SV_POSITION;
            float4 posWorld : TEXCOORD0;
            float4 posLocal : TEXCOORD1;
            // position of the vertex (and fragment) in world space
            float4 tex : TEXCOORD2;
            float3 tangentWorld : TEXCOORD3;
            float3 normalWorld : TEXCOORD4;
            float3 binormalWorld : TEXCOORD5;
        };

        vertexOutput vert(vertexInput input)
        {
            vertexOutput output;

            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject;

            output.tangentWorld = normalize(
                mul(modelMatrix, float4(input.tangent.xyz, 0.0)).xyz);
            output.normalWorld = normalize(
                mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
            output.binormalWorld = normalize(
                cross(output.normalWorld, output.tangentWorld) * input.tangent.w);
                // tangent.w is specific to Unity

            output.posWorld = mul(modelMatrix, input.vertex);
            output.tex = input.texcoord;
            output.pos = UnityObjectToClipPos(input.vertex);
            output.posLocal = mul(_InverseModelMatrix, mul(modelMatrix, input.vertex));

            return output;
        }

        // fragment shader with ambient lighting
        float4 fragWithAmbient(vertexOutput input) : COLOR
        {
            float4 encodedNormal = tex2D(_BumpMap,
                _BumpMap_ST.xy * input.tex.xy + _BumpMap_ST.zw);
            float3 localCoords = float3(2.0 * encodedNormal.a - 1.0,
                 2.0 * encodedNormal.g - 1.0, 0.0);
            localCoords.z = sqrt(1.0 - dot(localCoords, localCoords));

            float3x3 local2WorldTranspose = float3x3(
                input.tangentWorld, input.binormalWorld, input.normalWorld);
            float3 normalDirection = normalize(mul(localCoords, local2WorldTranspose));

            float3 viewDirection = normalize(_WorldSpaceCameraPos - input.posWorld.xyz);
            float3 lightDirection;
            float attenuation;

            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
                attenuation = 1.0; // no attenuation
                lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            }
            else // point or spot light
            {
                float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.posWorld.xyz;
                float distance = length(vertexToLightSource);
                attenuation = 1.0 / distance; // linear attenuation
                lightDirection = normalize(vertexToLightSource);
            }

            float3 ambientLighting = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;

            float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb
                * max(0.0, dot(normalDirection, lightDirection));

            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0)
            // light source on the wrong side?
            {
                specularReflection = float3(0.0, 0.0, 0.0);
                // no specular reflection
            }
            else // light source on the right side
            {
                specularReflection = attenuation * _LightColor0.rgb * _SpecColor.rgb
                    * pow(max(0.0, dot(reflect(-lightDirection, normalDirection),
                    viewDirection)), _Shininess);
            }

                float dist = distance(input.posLocal, float4(0.0, 0.0, 0.0, 1.0));
                    // computes the distance between the fragment position in
                    // the cutter's local coordinates and the cutter's origin

                if (dist < _CutterRadius)
                {
                    discard; // drop the fragment if it's inside the cutting sphere
                }

            return float4((ambientLighting + diffuseReflection) * tex2D(_MainTex, input.tex.xy)
                + specularReflection, 1.0);
        }
    ENDCG

    SubShader
    {
        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
            // first pass for ambient light and first light source
            Cull Back // render only front faces

            CGPROGRAM
                #pragma vertex vert
                #pragma fragment fragWithAmbient
                // the functions are defined in the CGINCLUDE part
            ENDCG
        }

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }
                // second pass for ambient light and first light source
            Cull Front // render only back faces

            CGPROGRAM
                #pragma vertex vert
                #pragma fragment fragWithAmbient
                // the functions are defined in the CGINCLUDE part
            ENDCG
        }
    }
    Fallback "Specular"
}
