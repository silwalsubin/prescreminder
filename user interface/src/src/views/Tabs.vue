<template>
  <ion-page>
    <ion-tabs>
      <ion-router-outlet></ion-router-outlet>
      <ion-tab-bar slot="bottom">
        <ion-tab-button tab="tab1" href="/tabs/dashboard" @click="initiateDashboard">
          <ion-icon :icon="homeOutline" />
        </ion-tab-button>
          
        <ion-tab-button tab="tab2" href="/tabs/prescriptions" @click="hapticsImpact">
          <ion-icon :icon="listCircleOutline" />
        </ion-tab-button>
        
        <ion-tab-button tab="tab4" href="/tabs/account" @click="hapticsImpact">
          <ion-icon :icon="personCircle" />
        </ion-tab-button>
      </ion-tab-bar>
    </ion-tabs>
  </ion-page>
</template>

<script lang="ts">
import { IonTabBar, IonTabButton, IonTabs, IonIcon, IonPage, IonRouterOutlet } from '@ionic/vue';
import { Haptics, ImpactStyle } from "@capacitor/haptics"
import {
  homeOutline,
  listCircleOutline,
  personCircle,
} from 'ionicons/icons';

import { useStore } from '@/store/store';

export default {
  name: 'Tabs',
  components: { IonTabs, IonTabBar, IonTabButton, IonIcon, IonPage, IonRouterOutlet, },
  setup() {
    const store = useStore();
    const hapticsImpact = async () => {
      await Haptics.vibrate();
    }
    const initiateDashboard = async () => {
      await store.dispatch('loadMedicationsToday');
      await hapticsImpact();
    }
    return {
      hapticsImpact,
      initiateDashboard,
      homeOutline,
      listCircleOutline,
      personCircle,
    }
  }
}
</script>