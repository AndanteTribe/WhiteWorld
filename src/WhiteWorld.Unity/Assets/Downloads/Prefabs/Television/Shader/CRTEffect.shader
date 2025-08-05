Shader "Unlit/CRTEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion ("Distortion", Float) = 0.2
        _ScanlineIntensity ("Scanline Intensity", Float) = 0.75
        _ScanlineFrequency ("Scanline Frequency", Float) = 1000.0
        _NoiseAmount ("Noise Amount", Float) = 0.05
        _ScanLineSpeed("ScanLine Speed", Float) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Distortion;
            float _ScanlineIntensity;
            float _ScanlineFrequency;
            float _NoiseAmount;
            float _ScanLineSpeed;

            float2 distort(float2 uv)
            {
                float2 cc = uv - 0.5;
                float dist = dot(cc, cc) * _Distortion;
                return (uv + cc * dist);
            }

            float random(float2 st)
            {
                return frac(sin(dot(st.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 distortedUV = distort(i.uv);
                
                fixed4 col = tex2D(_MainTex, distortedUV);

                float timeOffset = _Time.y * _ScanLineSpeed;
                float scanline = sin(i.uv.y * _ScanlineFrequency + timeOffset) * 0.5 + 0.5;
                col.rgb *= lerp(1.0, scanline, _ScanlineIntensity);

                float vignette = (1.0 - distance(i.uv, float2(0.5, 0.5)) * 1.2);
                col.rgb *= vignette;

                float noise = (random(i.uv + _Time.y) - 0.5) * _NoiseAmount;
                col.rgb += noise;
                
                float inBoundsX = step(0.0, distortedUV.x) * step(distortedUV.x, 1.0);
                float inBoundsY = step(0.0, distortedUV.y) * step(distortedUV.y, 1.0);
                float inBounds = inBoundsX * inBoundsY;
                
                col = lerp(fixed4(0, 0, 0, 1), col, inBounds);
                
                return col;
            }
            ENDCG
        }
    }
}