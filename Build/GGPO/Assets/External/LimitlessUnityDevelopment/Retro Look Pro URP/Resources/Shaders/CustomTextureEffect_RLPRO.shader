Shader "Hidden/Shader/CustomTextureEffect_RLPRO"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    HLSLINCLUDE

        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_CustomTex);
        SAMPLER(sampler_CustomTex);
	    half fade;
        half alpha;

    float4 CustomPostProcess(Varyings input) : SV_Target
    {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        float2 positionSS = input.uv;
        float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, positionSS);
        float4 col2 = SAMPLE_TEXTURE2D(_CustomTex, sampler_CustomTex, positionSS);
        return lerp(col, col2, col2.a * fade);
    }

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "#CustomTexture#"

			Cull Off ZWrite Off ZTest Always

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}