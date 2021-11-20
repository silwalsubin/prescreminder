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
        <prescription 
          v-for="prescription in prescriptions" 
          :key="prescription.prescriptionId"
          :prescription="prescription"
        />
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

import AddPrescriptionButton from '@/components/add-presciption-button.vue'
import { useStore } from '@/store/store'
import { onMounted, computed } from 'vue'
import Prescription from './presciption.vue'

export default  {
  name: 'PrescriptionsTab',
  components: {
    AddPrescriptionButton,
    IonContent,
    IonHeader,
    IonToolbar, 
    IonTitle,
    IonPage,
    Prescription,
  },
  setup() {
    const store = useStore();

    const prescriptions = computed(() => store.getters.prescriptions);

    onMounted(async () => {
      await store.dispatch('loadPrescriptions');
    })

    return {
      prescriptions,
    }
  }
}
</script>

<style scoped>
.prescriptions-tab-container {
  padding: 20px;
}
</style>