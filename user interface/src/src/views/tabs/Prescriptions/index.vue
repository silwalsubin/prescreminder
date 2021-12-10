<template>
  <ion-page>
    <tab-header header-title="My Prescriptions"/>
    <ion-content :fullscreen="true">
      <add-prescription-button 
        :blink="prescriptions.length === 0"
      />
      <share-prescription-button v-if="prescriptions.length > 0"/>
      <!-- <download-prescription-button v-if="prescriptions.length > 0"/> -->
      <blackboard v-if="prescriptions.length === 0"
        message="You do not have any prescriptions. Click the + button to create one."
        type="info"
      />
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
} from '@ionic/vue';

import AddPrescriptionButton from '@/components/add-presciption-button.vue'
import SharePrescriptionButton from "./share-prescriptions-button.vue"
import Blackboard from "@/components/wall/blackboard.vue"
// import DownloadPrescriptionButton from "./download-prescriptions-button.vue"
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { computed } from 'vue'
import Prescription from './presciption.vue'
export default  {
  name: 'PrescriptionsTab',
  components: {
    AddPrescriptionButton,
    Blackboard,
    // DownloadPrescriptionButton,
    SharePrescriptionButton,
    IonContent,
    IonPage,
    Prescription,
    TabHeader,
  },
  setup() {
    const store = useStore();

    const prescriptions = computed(() => store.getters.prescriptions);

    return {
      prescriptions,
    }
  }
}
</script>

<style scoped>
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