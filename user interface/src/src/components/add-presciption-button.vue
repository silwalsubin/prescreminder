<template>
  <ion-fab vertical="bottom" horizontal="end" slot="fixed">
    <ion-fab-button color="success">
      <ion-icon @click="openModal" :icon="add"></ion-icon>
    </ion-fab-button>
  </ion-fab>
</template>

<script lang="ts">
import { 
  IonFab,
  IonFabButton,
  IonIcon,
  modalController
} from '@ionic/vue';

import { add } from 'ionicons/icons';
import Modal from './add-prescription-model.vue';

export default {
  name: 'AddPrescriptionButton',
  components: {
    IonFab,
    IonFabButton,
    IonIcon
  },
  setup() {
    const openModal = async () => {
      const modal = await modalController.create({
        component: Modal,
        componentProps: {
          title: "Add Prescription",
        }
      });
      return modal.present();
    };

    return {
      add,
      openModal,
    }
  }
}
</script>