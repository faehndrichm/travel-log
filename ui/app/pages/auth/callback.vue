<!-- <script setup lang="ts">
import { useRouter } from '#imports'

const router = useRouter();
const { $keycloak } = useNuxtApp()

onMounted(async () => {

    // Fallback for fragment style (#)
    const params = new URLSearchParams(window.location.hash.slice(1))
    const code = params.get('code') || undefined
    const state = params.get('state') || undefined
    const sessionState = params.get('session_state') || undefined
    const codeVerifier = $keycloak.

    console.log('Retrieved code verifier from sessionStorage:', codeVerifier);
    console.log('Auth callback with code:', code, 'state:', state, 'session_state:', sessionState);
    if (!code) {
        console.error('No authorization code in callback URL')
        return router.replace('/auth/login');
    }

    try {
        await $fetch('/api/auth/keycloak/callback', {
            method: 'POST',
            body: { code, codeVerifier, state, session_state: sessionState }
        });

        router.replace('/');
    } catch (err) {
        console.error('Auth callback failed:', err);
        router.replace('/auth/login');
    }
})
</script>

<template>
    <div class="flex items-center justify-center h-screen">
        <p>Signing you in...</p>
    </div>
</template> -->
<template>
    <div>
        Waiting for redirect...
    </div>
</template>