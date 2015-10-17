// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:3,spmd:1,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:917,x:33735,y:32622,varname:node_917,prsc:2|diff-2920-OUT,spec-1052-OUT,gloss-9334-OUT,normal-6962-OUT,emission-7014-OUT;n:type:ShaderForge.SFN_Tex2d,id:7603,x:32065,y:32976,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_9565,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ComponentMask,id:6141,x:32336,y:32964,varname:node_6141,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7603-RGB;n:type:ShaderForge.SFN_Append,id:6962,x:32879,y:32896,varname:node_6962,prsc:2|A-5867-OUT,B-7603-B;n:type:ShaderForge.SFN_Multiply,id:5867,x:32655,y:32975,varname:node_5867,prsc:2|A-6141-OUT,B-5555-OUT;n:type:ShaderForge.SFN_Slider,id:5555,x:32228,y:33093,ptovrint:False,ptlb:N_Intensity,ptin:_N_Intensity,varname:node_1797,prsc:2,min:0,cur:0.1965812,max:1;n:type:ShaderForge.SFN_Tex2d,id:6009,x:31764,y:32764,ptovrint:False,ptlb:R,ptin:_R,varname:node_5792,prsc:2,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Vector1,id:44,x:32343,y:32516,varname:node_44,prsc:2,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1052,x:32582,y:32514,ptovrint:False,ptlb:Metal,ptin:_Metal,varname:node_382,prsc:2,on:False|A-44-OUT,B-8335-OUT;n:type:ShaderForge.SFN_Vector1,id:8335,x:32356,y:32560,varname:node_8335,prsc:2,v1:1;n:type:ShaderForge.SFN_Panner,id:6076,x:32748,y:33197,varname:node_6076,prsc:2,spu:1,spv:1;n:type:ShaderForge.SFN_Lerp,id:7068,x:32797,y:32514,varname:node_7068,prsc:2|A-7467-OUT,B-8720-OUT,T-8585-RGB;n:type:ShaderForge.SFN_Vector3,id:7467,x:32239,y:32271,varname:node_7467,prsc:2,v1:0.9338235,v2:0.4716032,v3:0.006866356;n:type:ShaderForge.SFN_Vector3,id:8720,x:32109,y:32383,varname:node_8720,prsc:2,v1:0.9960784,v2:0.7137255,v3:0.1372549;n:type:ShaderForge.SFN_Multiply,id:8465,x:32797,y:32650,varname:node_8465,prsc:2|A-7068-OUT,B-8585-RGB;n:type:ShaderForge.SFN_Panner,id:7035,x:32243,y:32698,varname:node_7035,prsc:2,spu:1,spv:1|DIST-4861-OUT;n:type:ShaderForge.SFN_Time,id:1786,x:31521,y:32346,varname:node_1786,prsc:2;n:type:ShaderForge.SFN_Sin,id:7265,x:31744,y:32403,varname:node_7265,prsc:2|IN-1786-TSL;n:type:ShaderForge.SFN_Multiply,id:4861,x:32021,y:32528,varname:node_4861,prsc:2|A-7265-OUT,B-846-OUT;n:type:ShaderForge.SFN_Vector1,id:846,x:31846,y:32598,varname:node_846,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:8585,x:32438,y:32737,ptovrint:False,ptlb:node_8585,ptin:_node_8585,varname:node_8585,prsc:2,tex:d23fcc71ed7444b498be22911927a712,ntxv:0,isnm:False|UVIN-7035-UVOUT;n:type:ShaderForge.SFN_Lerp,id:7014,x:33045,y:32669,varname:node_7014,prsc:2|A-8465-OUT,B-7068-OUT,T-779-OUT;n:type:ShaderForge.SFN_Vector1,id:9334,x:31968,y:32766,varname:node_9334,prsc:2,v1:1;n:type:ShaderForge.SFN_Sin,id:5737,x:31783,y:32263,varname:node_5737,prsc:2|IN-1786-T;n:type:ShaderForge.SFN_Multiply,id:779,x:32026,y:32263,varname:node_779,prsc:2|A-5737-OUT,B-846-OUT;n:type:ShaderForge.SFN_Multiply,id:2920,x:33301,y:32453,varname:node_2920,prsc:2|A-7068-OUT,B-7205-OUT;n:type:ShaderForge.SFN_Vector1,id:7205,x:33123,y:32485,varname:node_7205,prsc:2,v1:0.5;proporder:1052-7603-5555-6009-8585;pass:END;sub:END;*/

Shader "Shader Forge/M_Master" {
    Properties {
        [MaterialToggle] _Metal ("Metal", Float ) = 0
        _Normal ("Normal", 2D) = "bump" {}
        _N_Intensity ("N_Intensity", Range(0, 1)) = 0.1965812
        _R ("R", 2D) = "gray" {}
        _node_8585 ("node_8585", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _N_Intensity;
            uniform fixed _Metal;
            uniform sampler2D _node_8585; uniform float4 _node_8585_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = float3((_Normal_var.rgb.rg*_N_Intensity),_Normal_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - 1.0; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                UnityGI gi = UnityGlobalIllumination (d, 1, gloss, normalDirection);
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 node_1786 = _Time + _TimeEditor;
                float node_846 = 0.5;
                float2 node_7035 = (i.uv0+(sin(node_1786.r)*node_846)*float2(1,1));
                float4 _node_8585_var = tex2D(_node_8585,TRANSFORM_TEX(node_7035, _node_8585));
                float3 node_7068 = lerp(float3(0.9338235,0.4716032,0.006866356),float3(0.9960784,0.7137255,0.1372549),_node_8585_var.rgb);
                float node_7205 = 0.5;
                float3 diffuseColor = (node_7068*node_7205); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp( 0.0, 1.0, _Metal ), specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 node_7014 = lerp((node_7068*_node_8585_var.rgb),node_7068,(sin(node_1786.g)*node_846));
                float3 emissive = node_7014;
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _N_Intensity;
            uniform fixed _Metal;
            uniform sampler2D _node_8585; uniform float4 _node_8585_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = float3((_Normal_var.rgb.rg*_N_Intensity),_Normal_var.b);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 1.0 - 1.0; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 node_1786 = _Time + _TimeEditor;
                float node_846 = 0.5;
                float2 node_7035 = (i.uv0+(sin(node_1786.r)*node_846)*float2(1,1));
                float4 _node_8585_var = tex2D(_node_8585,TRANSFORM_TEX(node_7035, _node_8585));
                float3 node_7068 = lerp(float3(0.9338235,0.4716032,0.006866356),float3(0.9960784,0.7137255,0.1372549),_node_8585_var.rgb);
                float node_7205 = 0.5;
                float3 diffuseColor = (node_7068*node_7205); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp( 0.0, 1.0, _Metal ), specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
