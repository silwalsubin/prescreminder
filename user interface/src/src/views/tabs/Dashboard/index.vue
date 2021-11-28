<template>
  <ion-page>
    <tab-header header-title="Today's Medication List"/>
    <ion-content :fullscreen="true"> 
      <add-prescription-button v-if="medicationsToday.length === 0" />
      <div class="no-prescriptions" v-if="medicationsToday.length === 0">
        <ion-text color="medium">
        No medication list available since you do not have any prescriptions. Click the + button to create one.
        </ion-text>
      </div>
      <div class="dashboard-tab-container">
        <ion-list>
          <ion-item-sliding v-for="medicationToday in medicationsToday" :key="medicationToday">
              <medication-info :medicationInfo="medicationToday" />
              <ion-item-options side="end">
                <ion-item-option>Done</ion-item-option>
              </ion-item-options>
          </ion-item-sliding>
        </ion-list>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import AddPrescriptionButton from '@/components/add-presciption-button.vue'
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { onMounted, computed } from 'vue'
import MedicationInfo from './medication-info.vue'
import {
  IonList,
  IonItemSliding,
  IonItemOption,
  IonItemOptions,
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
    IonItemSliding,
    IonItemOption,
    IonItemOptions,
    IonList,
    IonPage, 
    IonText,
    MedicationInfo,
  },
  setup() {
    const store = useStore();

    const medicationsToday = computed(() => store.getters.medicationCheckListItems);

    onMounted(async () => {
      await store.dispatch('loadMedicationsToday');
    })

    return {
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