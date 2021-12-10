<template>
  <ion-page class="welcome-container">
    <div>
      <h1 class="welcome-message">Welcome to Prescreminder</h1>
      <div class="welcome-buttons">
        <ion-router-link href="/register">
          <ion-button 
            size="large" 
            shape="round" 
            color="success" 
            class="welcome-button"
          >
            Register
          </ion-button>
        </ion-router-link>
        <ion-router-link href="/login">
          <ion-button 
            size="large" 
            shape="round" 
            color="primary" 
            class="welcome-button"
          >
            Sign In
          </ion-button>
        </ion-router-link>
      </div>
    </div>
  </ion-page>
</template>

<script lang="ts">
import { 
    IonButton, 
    IonPage, 
} from '@ionic/vue';

export default {
  name: 'Welcome',
  components: { 
    IonButton, 
    IonPage, 
  },
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

<style scoped lang="scss">
@import "@/styles/animations/blinking-animation.scss";
.welcome-button {
  animation: $blink-default;
}
</style>