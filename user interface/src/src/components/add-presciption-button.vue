<template>
  <ion-fab vertical="bottom" horizontal="end" slot="fixed" :class="blink ? 'add-prescription-button--blink': ''">
    <ion-fab-button @click="setOpen(true)" color="success">
      <ion-icon :icon="add"></ion-icon>
    </ion-fab-button>
  </ion-fab>
  <ion-modal
    :is-open="isOpenRef"
    @didDismiss="setOpen(false)"
  >
    <Modal 
      @close="setOpen(false)"
      :isVisible="isOpenRef"
    />
  </ion-modal>
</template>

<script>
import { add } from 'ionicons/icons';
import { IonModal, IonFab, IonFabButton, IonIcon, } from '@ionic/vue';
import { ref } from 'vue';
import Modal from './add-edit-prescription-modal.vue'

export default {
  components: { IonModal, Modal, IonFab, IonFabButton, IonIcon, },
  props: {
    blink: { type: Boolean, default: false, required: false }
  },
  setup() {
    const isOpenRef = ref(false);
    const setOpen = (state) => isOpenRef.value = state;
    return {
      add,
      isOpenRef, 
      setOpen,  
    }
  }
};
</script>

<style scoped lang="scss">
@import "@/styles/animations/blinking-animation.scss";
.add-prescription-button--blink {
  animation: $blink-default;
}
</style>