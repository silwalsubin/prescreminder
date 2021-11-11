<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>My Prescriptions</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <div class="prescriptions-tab-container">
        My Prescriptions
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/vue';

export default  {
  name: 'PrescriptionsTab',
  components: { IonHeader, IonToolbar, IonTitle, IonContent, IonPage }
}
</script>

<style scoped>
.prescriptions-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>