 Shader "Vertex Colored/Unlit" {
     Properties {
         _Color ("Color", Color) = (1,1,1,1)
         _MainTex ("Albedo (RGB)", 2D) = "white" {}
     }
     SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
         lighting Off
         CGPROGRAM
         #pragma surface surf Lambert vertex:vert fullforwardshadows
         
       
         
         struct Input {
             float2 uv_MainTex;
             float3 vertexColor;
         };
         
         struct v2f {
           float4 pos : SV_POSITION;
           fixed4 color : COLOR;
         };
 
         void vert (inout appdata_full v, out Input o)
         {
             UNITY_INITIALIZE_OUTPUT(Input,o);
             o.vertexColor = v.color;
         }
 
         sampler2D _MainTex;
         
         fixed4 _Color;
 
         void surf (Input IN, inout SurfaceOutput o) 
         {
             //o.Albedo = IN.vertexColor;
             o.Emission = IN.vertexColor;
         }
         ENDCG
     } 
     FallBack "Diffuse"
 }