Shader "Unlit/OverlayVideo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off
        ZWrite Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                float4 bg = tex2D(_MainTex, i.uv); // optional, can be scene color
                // Overlay blend formula
                float3 result;
                for (int c = 0; c < 3; c++)
                {
                    result[c] = (bg[c] < 0.5) ? (2 * bg[c] * color[c]) : (1 - 2 * (1 - bg[c]) * (1 - color[c]));
                }
                return float4(result, color.a);
            }
            ENDCG
        }
    }
}