// https://nuxt.com/docs/api/configuration/nuxt-config

import tailwindcss from "@tailwindcss/vite";
import Keycloak from "keycloak-js";

export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },
  css: ['~/assets/css/main.css'],
  vite: {    
    plugins: [      
    tailwindcss(),
      ],  
    },
  modules: ['@nuxt/image', '@nuxt/ui', '@nuxt/eslint', 'nuxt-auth-utils'],
  nitro: {
      preset: 'bun',
   },
   runtimeConfig: {
    oauth: {
      keycloak:{
        clientId: 'frontend',
        
      }
    },
    public: {
      apiBase: '',
    },
  },

  // oidc: {
  //   devtools: true,
  //     middleware: {
  //       globalMiddlewareEnabled: true,
  //       customLoginPage: false
  //     },
  //     defaultProvider: 'keycloak',
  //     providers: {
  //       keycloak: {
  //         // exposeIdToken: true,
  //         // exposeAccessToken: true,
  //         audience: 'account',
  //         baseUrl: process.env.NUXT_OIDC_PROVIDERS_KEYCLOAK_BASE_URL,
  //         clientId: process.env.NUXT_OIDC_PROVIDERS_KEYCLOAK_CLIENT_ID,
  //         redirectUri: 'http://localhost:3000/auth/keycloak/callback',
  //           pkce: true,
  //           grantType: 'authorization_code',
  //           tokenRequestType: 'form-urlencoded'
  //       }
  //     }
  //   },
})