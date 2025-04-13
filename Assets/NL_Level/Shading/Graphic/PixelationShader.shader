Shader"Custom/PixelationPostProcessing"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _PixelSize ("Pixel Size", Range(1, 64)) = 8
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

uniform float _PixelSize;
sampler2D _MainTex;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

half4 frag(v2f i) : SV_Target
{
                // Pixelate the UVs by flooring the UV coordinates and dividing by _PixelSize
    i.uv = floor(i.uv * _PixelSize) / _PixelSize;
    return tex2D(_MainTex, i.uv);
}
            ENDCG
        }
    }
}
