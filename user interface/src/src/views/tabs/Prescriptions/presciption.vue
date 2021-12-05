<template>
  <form-item>
    <div class="prescription-information">
      <ion-label color="dark" position="stacked">
        {{prescription.name}}
      </ion-label>
      <ion-text color="medium">
        <p>{{prescription.unitDose}}</p>
      </ion-text>
    </div>
      <div class="prescription-actions" slot="end">
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
  </form-item>
</template>

<script>
import FormItem from "@/components/form-elements/form-item.vue";
import Modal from '@/components/add-edit-prescription-modal.vue'
import { notifyAsync, NotificationType } from '@/toast-notifications'
import PrescriptionViewModal from '@/store/view-models/prescription-view-modal'
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
  IonLabel,
  IonText,
} from '@ionic/vue';

export default {
  components: {
    FormItem,
    Modal,
    IonButton,
    IonIcon,
    IonLabel,
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
      const message = `${props.prescription.name} ${props.prescription.unitDose} deleted successfully`;
      await notifyAsync(NotificationType.Success, message);
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
