<template>
  <ion-page>
    <tab-header header-title="Reminders Today"/>
    <ion-content :fullscreen="true"> 
      <add-prescription-button v-if="prescriptions.length === 0" />
      <div class="no-prescriptions" v-if="medicationsToday.length === 0">
        <ion-text color="medium" v-if="prescriptions.length === 0">
        No medication list available since you do not have any prescriptions. Click the + button to create one.
        </ion-text>
        <ion-text color="medium" v-else>
        You do not have any medicine to intake now.
        </ion-text>
      </div>
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
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { computed } from 'vue'
import MedicationInfo from './medication-info.vue'
import {
  IonPage,
  IonContent,
  IonText,
} from '@ionic/vue';


export default  {
  name: 'DashboardTab',
  components: { 
    AddPrescriptionButton,
    IonContent, 
    TabHeader, 
    IonPage, 
    IonText,
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