<template>
  <ion-page>
    <ion-tabs>
      <ion-router-outlet></ion-router-outlet>
      <ion-tab-bar slot="bottom">
        <ion-tab-button tab="tab1" href="/tabs/dashboard" @click="initiateDashboard">
          <ion-icon :icon="homeOutline" />
        </ion-tab-button>
          
        <ion-tab-button tab="tab2" href="/tabs/prescriptions">
          <ion-icon :icon="listCircleOutline" />
        </ion-tab-button>

        <ion-tab-button tab="tab3" href="/tabs/history">
          <ion-icon :icon="statsChart" />
        </ion-tab-button>
        
        <ion-tab-button tab="tab4" href="/tabs/account">
          <ion-icon :icon="personCircle" />
        </ion-tab-button>
      </ion-tab-bar>
    </ion-tabs>
  </ion-page>
</template>

<script lang="ts">
import { IonTabBar, IonTabButton, IonTabs, IonIcon, IonPage, IonRouterOutlet } from '@ionic/vue';
import {
  homeOutline,
  listCircleOutline,
  personCircle,
  square, 
  statsChart,
  triangle 
} from 'ionicons/icons';

import { useStore } from '@/store/store';

export default {
  name: 'Tabs',
  components: { IonTabs, IonTabBar, IonTabButton, IonIcon, IonPage, IonRouterOutlet },
  setup() {
    const store = useStore();
    const initiateDashboard = async () => {
      await store.dispatch('loadMedicationsToday');
    }
    return {
      initiateDashboard,
      homeOutline,
      listCircleOutline,
      personCircle,
      square,
      statsChart,
      triangle,
    }
  }
}
</script>