<template>
  <ion-fab vertical="bottom" horizontal="start" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="medium">
      <ion-icon :icon="shareOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script lang="ts">
import { shareOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { useStore } from "@/store/store";
import { FileSharer } from '@byteowls/capacitor-filesharer';

export default {
  components: { IonFab, IonFabButton, IonIcon, },
  setup() {
    const store = useStore();
    const handleClickAsync = async () => {
      const response = await store.dispatch('getPrescriptionPdf')
      const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
      const reader = new FileReader();
      reader.onloadend = async () => {
        const result = reader.result as string;
        const base64Data = result.split(',')[1];
        await FileSharer.share({
          filename: fileName,
          base64Data: base64Data,
          contentType: 'application/pdf'
        })
      }
      reader.readAsDataURL(response.data);
    }
    return {
      handleClickAsync,
      shareOutline
    }
  }
};
</script>