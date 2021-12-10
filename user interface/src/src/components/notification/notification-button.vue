<template>
  <ion-button
    :class="hasNotifications ? 'has-notifications' : ''"
    fill="clear"
    size="large"
    @click='setOpen(true)'
  >
    <ion-icon 
      :color="hasNotifications ? 'danger': 'medium'" 
      :icon="hasNotifications ? notificationsSharp : notificationsOutline" />
    <ion-modal
      :is-open="isOpenRef"
      @didDismiss="setOpen(false)"
    >
      <Modal 
        @close="setOpen(false)"
        :isVisible="isOpenRef"
      />
    </ion-modal>
  </ion-button>
</template>

<script lang="ts">
import {
  IonButton,
  IonIcon,
  IonModal,
} from '@ionic/vue';

import {
  notificationsSharp,
  notificationsOutline
} from 'ionicons/icons';

import { useStore } from '@/store/store';
import { computed, ref } from 'vue';
import Modal from './notification-modal.vue'

export default  {
  name: 'LogIn',
  components: {
    IonButton,
    IonIcon,
    IonModal,
    Modal,
  },
  setup() {
    const store = useStore();
    const isOpenRef = ref(false);
    const setOpen = (state: boolean) => isOpenRef.value = state;

    const hasNotifications = computed(() => {
      return store.getters.notifications.length !== 0;
    });

    return {
      setOpen,
      hasNotifications,
      isOpenRef,
      notificationsSharp,
      notificationsOutline
    }
  }
}
</script>

<style scoped>
.has-notifications {
  animation: blinker 3s linear infinite;
}

@keyframes blinker {
  50% {
    opacity: 0.3;
  }
}
</style>