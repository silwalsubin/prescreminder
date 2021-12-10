<template>
  <ion-page>
    <tab-header header-title="Reminders Today"/>
    <ion-content :fullscreen="true"> 
      <add-prescription-button 
        v-if="prescriptions.length === 0"
        :blink="true"
      />
      <template v-if="medicationsToday.length === 0">
        <blackboard v-if="prescriptions.length === 0"
          message="No reminder available as you do not have any prescriptions. Click the + button to create one."
          type="info"
        />
        <blackboard 
          v-else
          message="You do not have any medicine to intake now."
          type="success"
        />
      </template>
      <medication-info 
        v-for="(medicationToday, index) in medicationsToday" 
        :key="index"
        :medicationInfo="medicationToday"
      />
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import AddPrescriptionButton from '@/components/add-presciption-button.vue'
import Blackboard from '@/components/wall/blackboard.vue'
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { computed } from 'vue'
import MedicationInfo from './medication-info.vue'
import {
  IonPage,
  IonContent,
} from '@ionic/vue';


export default  {
  name: 'DashboardTab',
  components: { 
    AddPrescriptionButton,
    Blackboard,
    IonContent, 
    TabHeader, 
    IonPage, 
    MedicationInfo,
  },
  setup() {
    const store = useStore();
    const medicationsToday = computed(() => store.getters.medicationCheckListItems);
    const prescriptions = computed(() => store.getters.prescriptions);
    return {
      prescriptions,
      medicationsToday,
    }
  }
}
</script>