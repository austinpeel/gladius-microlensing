Shader "Lensing/Microlensing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ImageSize ("Image Size, float", Range(0, 1)) = 1
        _ImagePosition ("Image Position", Vector) = (0.5, 0.5, 0, 0)
        _LensStrength ("Lens Strength", Range(0, 1)) = 0
        _LensPositionTex ("Lens Position Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_TexelSize;
            float _ImageSize;
            float2 _ImagePosition;
            float _LensStrength;
            float4 _LensPositionTex_TexelSize;
        CBUFFER_END

        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);

        TEXTURE2D(_LensPositionTex);
        SAMPLER(sampler_LensPositionTex);

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

            float4 frag(VertexOutput i) : SV_Target
            {
                float2 pos = 0.5 + (i.uv - 0.5 - _ImagePosition) / _ImageSize;
                // pos = saturate(pos);

                // Compute lensing deflection angles
                float2 alpha = float2(0, 0);

                float pixel_width = _LensPositionTex_TexelSize.x;
                int width = _LensPositionTex_TexelSize.z;
                for (int j = 0; j < width; j++)
                {
                    float2 uv_pixel = float2((0.5 + j) * pixel_width, 0.5);
                    float4 pos = SAMPLE_TEXTURE2D(_LensPositionTex, sampler_LensPositionTex, uv_pixel);
                    float2 xy = i.uv - pos.xy;
                    float r2 = xy.x * xy.x + xy.y * xy.y;
                    alpha += 0.015 * _LensStrength * xy / r2;
                }

                float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, pos - alpha);
                return color;
            }

            ENDHLSL
        }
    }
}
