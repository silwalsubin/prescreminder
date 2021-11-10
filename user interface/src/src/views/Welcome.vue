<template>
  <ion-page class="welcome-container">
    <div>
      <h1 class="welcome-message">Welcome to Prescreminder</h1>
      <div class="welcome-buttons">
        <ion-router-link href="/tabs/">
          <ion-button size="large" shape="round" color="success">Register</ion-button>
        </ion-router-link>
        <ion-button size="large" shape="round" color="primary">Sign In</ion-button>
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