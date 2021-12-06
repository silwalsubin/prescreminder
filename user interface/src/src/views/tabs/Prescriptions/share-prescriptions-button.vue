<template>
  <ion-fab vertical="bottom" horizontal="center" slot="fixed">
    <ion-fab-button @click="handleClickAsync" color="light">
      <ion-icon :icon="shareOutline"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script>
import { shareOutline } from 'ionicons/icons';
import { IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { useStore } from "@/store/store";
import { Share } from "@capacitor/share"; 

export default {
  components: { IonFab, IonFabButton, IonIcon, },
  setup() {
    const store = useStore();
    const handleClickAsync = async () => {
      await Share.share({
        title: 'See cool stuff',
        text: 'Really awesome thing you need to see right meow',
        url: 'http://ionicframework.com/',
        dialogTitle: 'Share with buddies',
      });
    }
    return {
      handleClickAsync,
      shareOutline
    }
  }
};
</script>