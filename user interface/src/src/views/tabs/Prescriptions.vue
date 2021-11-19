<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>My Prescriptions</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">
      <add-prescription-button />
      <div class="prescriptions-tab-container">
        My Prescriptions
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import { 
  IonContent,
  IonHeader,
  IonToolbar,
  IonTitle,
  IonPage, 
} from '@ionic/vue';

import AddPrescriptionButton from '../../components/add-presciption-button.vue'

export default  {
  name: 'PrescriptionsTab',
  components: {
    AddPrescriptionButton,
    IonContent,
    IonHeader,
    IonToolbar, 
    IonTitle,
    IonPage, 
  },
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