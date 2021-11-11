<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Prescreminder</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">  
      <div class="dashboard-tab-container">
      Dashboard
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { IonPage, IonHeader, IonToolbar, IonTitle, IonContent } from '@ionic/vue';

export default  {
  name: 'DashboardTab',
  components: { IonHeader, IonToolbar, IonTitle, IonContent, IonPage }
}
</script>

<style scoped>
.dashboard-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>