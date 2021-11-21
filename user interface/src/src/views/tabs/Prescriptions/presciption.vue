<template>
  <div class="prescription-card">
    <div class="prescription-information">
      <ion-text color="dark">
        <h4>{{prescription.name}}</h4>
      </ion-text>
      <ion-text color="dark">
        <p>{{prescription.quantity}}</p>
      </ion-text>
    </div>
    <div class="prescription-actions">
      <div class="precription-actions-buttons">
        <ion-button
          fill="clear"
          color="success"
        >
          <ion-icon :icon="createOutline" />
        </ion-button>
        <ion-button
          fill="clear"
          color="danger"
          @click="handleDelete"
        >
          <ion-icon :icon="trashOutline" />
        </ion-button>
      </div>
    </div>
  </div>
</template>

<script>
import PrescriptionViewModal from '@/store/view-models/prescription-view-modal'
import {
  createOutline,
  trashOutline,
} from 'ionicons/icons';

import { useStore } from '@/store/store';

import {
  IonButton,
  IonIcon,
  IonText,
  toastController,
} from '@ionic/vue';

export default {
  components: {
    IonButton,
    IonIcon,
    IonText,
  },
  props: {
    prescription: { type: PrescriptionViewModal, required: true }
  },
  setup(props) {
    const store = useStore();

    const handleDelete = async () => {
      await store.dispatch('deletePrescription', props.prescription.prescriptionId);
      const toast = await toastController.create({
        message: `${props.prescription.name} ${props.prescription.quantity} deleted successfully`,
        duration: 2000,
        color: "success", 
        animated: true
      });
      toast.present();
    }
    return {
      handleDelete,
      createOutline,
      trashOutline,
    }
  }
}
</script>

<style scoped>
.prescription-card {
  display: flex;
  margin-bottom: 10px;
  border-radius: 10px;
  border: 1px solid grey;
}

.prescription-information {
  padding-left: 10px;
  flex: 4;
}

.prescription-actions {
  flex: 1;
}

.precription-actions-buttons {
  max-width: 50px;
  float: right;
}
</style>