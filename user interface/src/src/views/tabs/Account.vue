<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>My Account</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">   
      <div class="account-tab-container">
        <ion-router-link href="/">
          <ion-button 
            size="large" 
            shape="round" 
            color="primary"
            :disabled="false"
          >
          Sign Out
          </ion-button>
        </ion-router-link>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import {
  IonButton,
  IonPage,
  IonHeader, 
  IonToolbar, 
  IonTitle, 
  IonContent
} from '@ionic/vue';

export default  {
  name: 'AccountTab',
  components: { 
    IonButton,
    IonHeader, 
    IonToolbar, 
    IonTitle, 
    IonContent, 
    IonPage, 
  }
}
</script>

<style scoped>
.account-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>