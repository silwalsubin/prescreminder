<template>
  <ion-fab vertical="bottom" horizontal="start" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="primary">
      <ion-icon :icon="cloudDownloadOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script>
import { cloudDownloadOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { useStore } from "@/store/store";

export default {
  components: { IonFab, IonFabButton, IonIcon, },
  setup() {
    const store = useStore();
    const handleClickAsync = async () => {
      await store.dispatch('downloadPrescriptionPdf')
    }
    return {
      handleClickAsync,
      cloudDownloadOutline
    }
  }
};
</script>