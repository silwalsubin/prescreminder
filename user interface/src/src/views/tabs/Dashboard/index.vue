<template>
  <ion-page>
    <tab-header header-title="Today's Medication List"/>
    <ion-content :fullscreen="true">  
      <div class="dashboard-tab-container">
        <medication-info :medicationInfo="medicationToday" v-for="medicationToday in medicationsToday" :key="medicationToday"/>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import TabHeader from '@/components/tabs/tab-header.vue'
import { useStore } from '@/store/store'
import { onMounted, computed } from 'vue'
import MedicationInfo from './medication-info.vue'
import {
  IonPage,
  IonContent,
} from '@ionic/vue';


export default  {
  name: 'DashboardTab',
  components: { 
    IonContent, 
    TabHeader, 
    IonPage, 
    MedicationInfo,
  },
  setup() {
    const store = useStore();

    const medicationsToday = computed(() => store.getters.medicationsToday);

    onMounted(async () => {
      await store.dispatch('loadMedicationsToday');
    })

    return {
      medicationsToday,
    }
  }
}
</script>