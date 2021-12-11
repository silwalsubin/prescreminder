<template>
  <ion-fab vertical="bottom" horizontal="start" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="medium">
      <ion-icon :icon="shareOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
  <ion-loading
    :is-open="isOpenRef"
    message="Generating Pdf..."
  />
</template>

<script lang="ts">
import { shareOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, IonLoading } from '@ionic/vue';
import { useStore } from "@/store/store";
import { FileSharer } from '@byteowls/capacitor-filesharer';
import { ref } from "vue";

export default {
  components: { IonFab, IonFabButton, IonIcon, IonLoading},
  setup() {
    const store = useStore();
    const isOpenRef = ref(false);
    const handleClickAsync = async () => {
      isOpenRef.value = true;
      const response = await store.dispatch('getPrescriptionPdf');
      isOpenRef.value = false;
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
      isOpenRef,
      handleClickAsync,
      shareOutline
    }
  }
};
</script>