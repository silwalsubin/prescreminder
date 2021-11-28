<template>
  <ion-page>
    <tab-header header-title="Today's Medication List"/>
    <ion-content :fullscreen="true">  
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
} from '@ionic/vue';


export default  {
  name: 'DashboardTab',
  components: { 
    IonContent, 
    TabHeader, 
    IonItemSliding,
    IonItemOption,
    IonItemOptions,
    IonList,
    IonPage, 
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