Shader "Lensing/PointSources"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Strength ("Strength", Range(0, 0.1)) = 0
        _ScaleFactor ("ScaleFactor", Float) = 1
        _X0 ("X0", Float) = 0.5
        _Y0 ("Y0", Float) = 0.5
        _PositionTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float _Strength;
            float _ScaleFactor;
            float _X0;
            float _Y0;
        CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        TEXTURE2D(_PositionTex);
        SAMPLER(sampler_PositionTex);

        struct VertexInput
        {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VertexOutput
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        ENDHLSL

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            VertexOutput vert(VertexInput i)
            {
                VertexOutput o;
                o.position = TransformObjectToHClip(i.position.xyz);
                o.uv = i.uv;
                return o;
            }

            // static const float x0[2] = {1.0, 0.5};
            // static const float y0[2] = {1.0, 0.5};
            static const float _xx[50] = {-5.86325,   7.73263, 1.91267,   6.94573,  -0.33579,  -2.97493,   9.16998,   8.79194, 2.94710,  -3.75307,   0.06554,  -8.76489,  -8.90370,  -5.49278, 8.87232,   4.92588,   9.58402,   8.93881,  -3.92979,  4.65179, 9.75598,  -8.82821,   1.32815,  -9.43098,  -6.79323,   3.09924, -10.28880,  -7.60164,   4.22388,   4.17965,  -0.76177,  -1.11611, -9.23649,  -5.93643,  -1.39999,   8.36296,   1.92418,  -4.03952, 7.49439,  -0.18252,  -1.65411,  -8.84003,   2.75366,   6.26092, 3.17912,  10.20920,  -6.22973,  -4.90661,  -2.96547,   1.07860};
            static const float _yy[50] = {-4.67191,  -7.77686, -6.85410,  -7.30008,  -7.78459,  -0.18478,  -5.24902,  -6.25314, 1.58933,  -8.84466,   1.50699,   3.11699,  -0.38136,   3.95107, 5.81144,   9.11473,  -2.73554,   6.23594,  -6.83720,  0.03397, 4.83714,  -6.50238,   4.59360,   0.23545,  -5.04880,  -1.15811, -0.73228,  -2.28455,   0.77617,  -5.08801,  -4.99638,   9.72844, -1.03108,   8.19101,   6.02728,  -2.75185,   2.21735,  -1.76288, 5.18637,  -6.43698,  -3.89563,  -1.58948,   2.65422,  -0.26763, 5.53124,   0.35701,  -4.48037,   9.37297,  -2.07391,  -5.87786};

            float4 frag(VertexOutput i) : SV_Target
            {
                _X0 *= _ScaleFactor;
                _Y0 *= _ScaleFactor;

                float2 alpha = float2(0, 0);
                for (int j = 0; j < 50; j++)
                {
                    // float4 posTex = SAMPLE_TEXTURE2D(_PositionTex, sampler_PositionTex, float2(0.25 * j, 0));
                    float u0 = (_ScaleFactor * _xx[j] + 12) / 24;
                    float v0 = (_ScaleFactor * _yy[j] + 12) / 24;
                    // float2 xy = i.uv - (float2(x0[j], y0[j]) + 0.5 - float2(_X0, _Y0));
                    float2 xy = i.uv - (float2(u0, v0) + 0.5 * _ScaleFactor - float2(_X0, _Y0));
                    float r2 = xy.x * xy.x + xy.y * xy.y;
                    alpha += _Strength * xy / r2;
                }

                // for (int j = 0; j < 4; j++)
                // {
                //     for (int k = 1; k < 4; k++)
                //     {
                //         posTex = SAMPLE_TEXTURE2D(_PositionTex, sampler_PositionTex, float2(0.25 * j, 0.25 * k));
                //         offset = posTex.xy - float2(0.5, 0.5);

                //         xy = i.uv - float2(_X0, _Y0) + offset;
                //         r = sqrt(xy.x * xy.x + xy.y * xy.y);
                //         alpha += _Strength * xy / r;
                //     }
                // }

                float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv - alpha);
                return col;
            }

            ENDHLSL
        }
    }
}
