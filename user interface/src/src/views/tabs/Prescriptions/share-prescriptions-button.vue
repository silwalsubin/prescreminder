<template>
  <ion-fab vertical="bottom" horizontal="start" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="primary">
      <ion-icon :icon="shareOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script>
import { shareOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { useStore } from "@/store/store";

export default {
  components: { IonFab, IonFabButton, IonIcon, },
  setup() {
    const store = useStore();
    const handleClickAsync = async () => {
      await store.dispatch('getPrescriptionsPdf')
    }
    return {
      handleClickAsync,
      shareOutline
    }
  }
};
</script>