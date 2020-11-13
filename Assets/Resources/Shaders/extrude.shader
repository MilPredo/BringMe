Shader "Custom/extrude" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Slider ("Extrude", Range (-1, 1)) = 0
        [Toggle] _FlipNormals ("Flip Normals", Float) = 1
    }
    SubShader {
        //Tags { "RenderType"="Opaque" }
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert nos

        struct Input {
            float2 uv_MainTex;
        };

        float _Slider;
        sampler2D _MainTex;
        struct appdata {
            float4 vertex: POSITION;
            float3 normal: NORMAL;
            float4 texcoord: TEXCOORD0;
        };

        void vert (inout appdata v){
            v.normal.xyz = v.normal * _FlipNormals;
            //v.vertex.xyz += v.normal * _Slider;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
