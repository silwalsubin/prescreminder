<template>
  <form-item>
    <div class="prescription-information">
      <ion-label color="dark" class="precription-unit-dose-badge">
        {{prescription.name}} {{prescription.unitDose}}
      </ion-label>
      <div class="prescription-content">
        <ion-chip :outline="false" :disabled="true">
          {{prescription.totalQuantity}} remaining
        </ion-chip>
      </div>
    </div>
    <div class="prescription-actions" slot="end">
      <ion-button
        fill="clear"
        class="prescription-action-button"
        color="primary"
        @click="handleActionClick"
      >
        <ion-icon
          size="large"
          :icon="ellipsisHorizontalCircleSharp" 
        />
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
  </form-item>
</template>

<script>
import FormItem from "@/components/form-elements/form-item.vue";
import Modal from '@/components/add-edit-prescription-modal.vue'
import { notifyAsync, NotificationType } from '@/toast-notifications'
import PrescriptionViewModal from '@/store/view-models/prescription-view-modal'
import {
  ellipsisHorizontalCircleSharp,
} from 'ionicons/icons';

import { useStore } from '@/store/store';
import { computed, ref } from 'vue';

import {
  IonButton,
  IonModal,
  IonIcon,
  IonLabel,
  IonChip,
  actionSheetController,
  alertController,
} from '@ionic/vue';

export default {
  components: {
    FormItem,
    Modal,
    IonButton,
    IonIcon,
    IonLabel,
    IonModal,
    IonChip,
  },
  props: {
    prescription: { type: PrescriptionViewModal, required: true }
  },
  setup(props) {
    const store = useStore();
    const isOpenRef = ref(false);
    const setOpen = (state) => isOpenRef.value = state;

    const itemLabel = computed(() => `${props.prescription.name} ${props.prescription.unitDose}`);

    const handleActionClick = async () => {
      const actionSheet = await actionSheetController.create({
          header: `${itemLabel.value}`,
          buttons: [
            {
              text: 'Edit',
              handler: () => {
                setOpen(true);
              },
            },
            {
              text: 'Delete',
              role: 'destructive',
              handler: async () => {
                const alert = await alertController.create({
                  header: 'Are you Sure?',
                  message: `This will delete ${itemLabel.value} and all its associated data`,
                  buttons: [
                    {
                      text: 'Cancel',
                      role: 'cancel',
                      handler: () => {
                        // do nothing 
                      },
                    },
                    {
                      text: 'Continue',
                      handler: async () => {
                        await store.dispatch('deletePrescription', props.prescription.prescriptionId);
                        const message = `${itemLabel.value} deleted successfully`;
                        await notifyAsync(NotificationType.Success, message);
                      },
                    },
                  ],
                });
                await alert.present();
              },
            },
            {
              text: 'Cancel',
              role: 'cancel',
              handler: () => {
                // do nothing
              },
            },
          ],
        });
        await actionSheet.present();
    }
    return {
      itemLabel,
      isOpenRef, 
      setOpen,  
      handleActionClick,
      ellipsisHorizontalCircleSharp,
    }
  }
}
</script>

<style scoped lang="scss">
@import "@/styles/animations/blinking-animation.scss";
.prescription-information {
  padding-bottom: 5px;
}

.prescription-action-button {
  margin-top: -10px;
  margin-right: -13px;
  animation: $blink-light;
}

.prescription-content {
  margin-left: -5px;
}
</style>
