Shader "Cg/Transparent/Cg shader with two passes using discard" {
   SubShader {

      // first pass (is executed before the second pass)
      Pass {
         Cull Front // cull only front faces

         CGPROGRAM

         #pragma vertex vert  
         #pragma fragment frag 
 
         float4 vert(float4 vertexPos : POSITION) : SV_POSITION 
         {
            return UnityObjectToClipPos(vertexPos);
         }
 
         float4 frag(void) : COLOR 
         {
            return float4(1.0, 0.0, 0.0, 1.0); // red
         }
 
         ENDCG  
      }

      // second pass (is executed after the first pass)
      Pass {
         Cull Back // cull only back faces

         CGPROGRAM

         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc" 
            // defines unity_ObjectToWorld and unity_WorldToObject

         // uniforms corresponding to properties
         uniform float _CuttingSphereRadius;
         uniform float4x4 _InverseModelMatrix;

         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_local_space : TEXCOORD0;
         };

         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output; 
 
            output.pos = UnityObjectToClipPos(input.vertex);
            output.position_in_local_space = 
               mul(_InverseModelMatrix, mul(unity_ObjectToWorld, input.vertex));

            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
            float dist = distance(input.position_in_local_space, float4(0.0, 0.0, 0.0, 1.0));
               // computes the distance between the fragment position
               // in the sphere's local coordinates and its origin
            
            if (dist < _CuttingSphereRadius) 
            {
               discard; // drop the fragment if it's inside the sphere
            }

            return float4(0.0, 1.0, 0.0, 1.0); // green
         }
 
         ENDCG  
      }
   }
}
