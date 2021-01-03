Shader "WitchOS/Color Palette"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _BlackColor ("Black Color", Color) = (0, 0, 0, 1)
        _WhiteColor ("White Color", Color) = (1, 1, 1, 1)
        _RampTex ("Magic Ramp LUT", 2D) = "defaulttexture" {}
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

            sampler2D _MainTex, _RampTex;
            float4 _BlackColor, _WhiteColor;

            inline fixed4 convertBlackAndWhite (fixed4 input)
            {
                bool closerToWhite = round(input.r);
                return closerToWhite ? _WhiteColor : _BlackColor;
            }

            // input is assumed to be somewhere on a lerp between (0, 1, 0) and (1, 1, 0)
            // these colors are chosen so that one channel goes from 0 to 1, the other two channels are always the same value, and all three channels are never equal
            inline fixed4 convertMagicRamp (fixed4 input)
            {
                float i = input.r;
                
                // for magic pixels that break the g or b assumptions, we can assume they occupy subpixel positions and that the pixel perfect camera averaged their true value with the background pixel. we'll fix that by making the magic effects override the background color
                if (input.g == 0.5)
                {
                    // undo average with black
                    i *= 2;
                }
                else if (input.b == 0.5)
                {
                    // undo average with white
                    i = i * 2 - 1;
                }

                return tex2D(_RampTex, i);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                return (col.r == col.g && col.g == col.b) ? convertBlackAndWhite(col) : convertMagicRamp(col);
            }
            ENDCG
        }
    }
}
