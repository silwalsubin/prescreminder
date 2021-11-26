<template>
  <ion-page class="welcome-container">
    <div>
      <h1 class="welcome-message">Welcome to Prescreminder</h1>
      <div class="welcome-buttons">
        <ion-button 
          size="large" 
          shape="round" 
          color="success"
          @click="handleRegister"
        >
        Register
        </ion-button>
        <ion-button 
          size="large" 
          shape="round" 
          color="primary"
          @click="handleLogin"
        >
        Sign In
        </ion-button>
      </div>
    </div>
  </ion-page>
</template>

<script lang="ts">
import { 
    IonButton, 
    IonPage, 
} from '@ionic/vue';

import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';

export default {
  name: 'Welcome',
  components: { 
    IonButton, 
    IonPage, 
  },
  setup() {
    const router = useRouter();

    const handleLogin = () => {
      router.push({
        name: RouteName.LogInPage
      })
    }

    const handleRegister = () => {
      router.push({
        name: RouteName.RegisterPage
      })
    }

    return {
      handleLogin,
      handleRegister,
    }
  }
}
</script>

<style scoped>
.welcome-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.welcome-message {
  text-align: center;
}

.welcome-buttons {
  text-align: center;
}

.welcome-buttons > *:not(:first-child)  {
  margin-left: 10px;
}
</style>