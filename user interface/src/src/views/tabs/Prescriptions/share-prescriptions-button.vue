<template>
  <ion-fab vertical="bottom" horizontal="center" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="light">
      <ion-icon :icon="shareOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script lang="ts">
import { shareOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { useStore } from "@/store/store";
import { FileSharer } from '@byteowls/capacitor-filesharer';
import { Share } from "@capacitor/share"; 

export default {
  components: { IonFab, IonFabButton, IonIcon, },
  setup() {
    const store = useStore();
    const handleClickAsync = async () => {
      const response = await store.dispatch('getPrescriptionPdf')
      const reader = new FileReader();
      reader.onloadend = async () => {
        const result = reader.result as string;
        const base64Data = result.split(',')[1];

        await FileSharer.share({
          filename: "test.pdf",
          base64Data: base64Data,
          contentType: 'application/pdf'
        })
      }
      reader.readAsDataURL(response.data);
      // await Share.share({
      //   title: 'See cool stuff',
      //   text: 'Really awesome thing you need to see right meow',
      //   url: 'http://ionicframework.com/',
      //   dialogTitle: 'Share with buddies',
      // });
    }
    return {
      handleClickAsync,
      shareOutline
    }
  }
};
</script>