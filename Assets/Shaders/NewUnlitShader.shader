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
                float l =  length(mul(unity_ObjectToWorld, v.vertex));
                o.color.xyz = float3(l,l,l);

                o.color.w = 1.0;

                o.pos = o.pos*l/o.pos.w;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    } 
}