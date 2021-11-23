<template>
  <div class="prescription-card">
    <div class="prescription-information">
      <ion-text color="dark">
        <h4>{{prescription.name}}</h4>
      </ion-text>
      <ion-text color="medium">
        <p>{{prescription.quantity}}</p>
      </ion-text>
    </div>
    <div class="prescription-actions">
      <div class="precription-actions-buttons">
        <ion-button
          fill="clear"
          color="success"
          @click="setOpen(true)"
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
        <ion-modal
          :is-open="isOpenRef"
          @didDismiss="setOpen(false)"
        >
          <Modal 
            :prescriptionId="prescription.prescriptionId"
            @close="setOpen(false)"
            :isVisible="isOpenRef"
          />
        </ion-modal>
      </div>
    </div>
  </div>
</template>

<script>
import PrescriptionViewModal from '@/store/view-models/prescription-view-modal'
import Modal from '@/components/add-edit-prescription-model.vue'
import {
  createOutline,
  trashOutline,
} from 'ionicons/icons';

import { useStore } from '@/store/store';
import { ref } from 'vue';

import {
  IonButton,
  IonModal,
  IonIcon,
  IonText,
  toastController,
} from '@ionic/vue';

export default {
  components: {
    Modal,
    IonButton,
    IonIcon,
    IonModal,
    IonText,
  },
  props: {
    prescription: { type: PrescriptionViewModal, required: true }
  },
  setup(props) {
    const store = useStore();
    const isOpenRef = ref(false);
    const setOpen = (state) => isOpenRef.value = state;
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
      isOpenRef, 
      setOpen,  
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