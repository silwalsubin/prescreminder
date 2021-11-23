<template>
  <ion-page>
    <tab-header header-title="My Prescriptions"/>
    <ion-content :fullscreen="true">
      <add-prescription-button />
      <div class="no-prescriptions" v-if="prescriptions.length === 0">
        <ion-text color="medium">
        You do not have any prescriptions. Click the + button to create one.
        </ion-text>
      </div>
      <div v-else class="prescriptions-tab-container">
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
  IonPage,
  IonText,
} from '@ionic/vue';

import AddPrescriptionButton from '@/components/add-presciption-button.vue'
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { onMounted, computed } from 'vue'
import Prescription from './presciption.vue'
export default  {
  name: 'PrescriptionsTab',
  components: {
    AddPrescriptionButton,
    IonContent,
    IonPage,
    IonText,
    Prescription,
    TabHeader,
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

.no-prescriptions {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  padding-left: 10px;
  padding-right: 10px;
  text-align: center;
}
</style>