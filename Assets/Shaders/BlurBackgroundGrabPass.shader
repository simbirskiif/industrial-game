Shader "Custom/BlurBackgroundGrabPass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
        _Downsample ("Downsample", Int) = 2
        _Iterations ("Iterations", Int) = 3
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        GrabPass
        {
            "_BackgroundTexture"
        }

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
                float4 grabPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BackgroundTexture;
            float _BlurSize;
            int _Downsample;
            int _Iterations;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Downsample
                float2 grabUV = i.grabPos.xy / i.grabPos.w;
                grabUV = floor(grabUV * _ScreenParams.xy / _Downsample) / (_ScreenParams.xy / _Downsample);

                // Blur
                float2 texelSize = 1.0 / _ScreenParams.xy;
                fixed4 col = fixed4(0,0,0,0);

                for (int iteration = 0; iteration < _Iterations; iteration++)
                {
                    float offset = _BlurSize * (iteration + 1);

                    // Vertical
                    for (float j = -offset; j <= offset; j++)
                    {
                        col += tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(float4(grabUV + float2(0, j * texelSize.y), i.grabPos.z, i.grabPos.w)));
                    }
                    col /= (offset * 2.0 + 1.0);

                    // Horizontal
                    fixed4 tempCol = fixed4(0,0,0,0);
                    for (float j = -offset; j <= offset; j++)
                    {
                        tempCol += tex2Dproj(_BackgroundTexture, UNITY_PROJ_COORD(float4(grabUV + float2(j * texelSize.x, 0), i.grabPos.z, i.grabPos.w)));
                    }
                    col = (col + tempCol / (offset * 2.0 + 1.0)) / 2;
                }

                return col;
            }
            ENDCG
        }
    }
}