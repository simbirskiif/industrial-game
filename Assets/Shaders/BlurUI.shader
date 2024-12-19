Shader "Custom/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
        _BlurredTex ("Blurred Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass // 0 - Capture
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

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }

        Pass // 1 - Vertical Blur
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

            sampler2D _BlurredTex;
            float _BlurSize;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 texelSize = 1.0 / _ScreenParams.xy;
                fixed4 col = fixed4(0,0,0,0);
                for (float j = -_BlurSize; j <= _BlurSize; j++)
                {
                    col += tex2D(_BlurredTex, i.uv + float2(0, j * texelSize.y));
                }
                col /= (_BlurSize * 2.0 + 1.0);
                return col;
            }
            ENDCG
        }

        Pass // 2 - Horizontal Blur
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

            sampler2D _BlurredTex;
            float _BlurSize;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 texelSize = 1.0 / _ScreenParams.xy;
                fixed4 col = fixed4(0,0,0,0);
                for (float j = -_BlurSize; j <= _BlurSize; j++)
                {
                    col += tex2D(_BlurredTex, i.uv + float2(j * texelSize.x, 0));
                }
                col /= (_BlurSize * 2.0 + 1.0);
                return col;
            }
            ENDCG
        }
    }
}