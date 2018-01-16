// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "VertexInputSimple" {
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
         
            struct v2f {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
            };
            
            v2f vert (appdata_base v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
               	float4 posw =  mul(unity_ObjectToWorld, v.vertex);
                o.color.xyz = float3(1.0,1.0,1.0);
                o.color.w = 1.0;

                o.pos = o.pos*(10.0*length(posw.xyz-_WorldSpaceCameraPos)/length(_WorldSpaceCameraPos))/o.pos.w;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    } 
}