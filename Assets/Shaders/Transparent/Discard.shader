Shader "Cg/Transparent/Cg shader using discard" {
   SubShader {
      Pass {
         Cull Off // turn off triangle culling, alternatives are:
         // Cull Back (or nothing): cull only back faces 
         // Cull Front : cull only front faces
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         struct vertexInput {
            float4 vertex : POSITION;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_world_space : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            output.pos = UnityObjectToClipPos(input.vertex);
            output.position_in_world_space = mul(unity_ObjectToWorld, input.vertex); 
 
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR 
         {
            if (input.position_in_world_space.y > 0.0) 
            {
               discard; // drop the fragment if y coordinate > 0
            }
            return float4(0.0, 1.0, 0.0, 1.0); // green
         }
 
         ENDCG  
      }
   }
}
