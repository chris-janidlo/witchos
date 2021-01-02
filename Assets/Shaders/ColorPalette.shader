Shader "WitchOS/Color Palette"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _BlackColor ("Black Color", Color) = (0, 0, 0, 1)
        _WhiteColor ("White Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _BlackColor, _WhiteColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // ignore anything that isn't grayscale
                if (!(col.r == col.g && col.g == col.b)) return col;

                bool closerToWhite = round(col.r);
                col.rgb = (closerToWhite ? _WhiteColor : _BlackColor).rgb;

                return col;
            }

            // TODO: transform everything on the magic effect color ramp to arbitrary, user-supplied color ramp
            // make sure that the transformation includes anything that's been mixed with pure white or pure black, by using the replacement colors
            ENDCG
        }
    }
}
