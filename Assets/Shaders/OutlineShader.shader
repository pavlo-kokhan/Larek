Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _OutlineColor;
        float _OutlineThickness;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _OutlineColor;
            o.Albedo = c.rgb;
        }
        ENDCG
    }
}
