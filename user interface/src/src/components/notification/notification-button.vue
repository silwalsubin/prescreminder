<template>
  <ion-button
    class="notification-button"
    fill="clear"
    @click='setOpen(true)'
  >
    <ion-icon :icon="notificationsOutline" />
    <ion-label position="stacked">
      <ion-badge :color="numberOfNotifications > 0 ? 'danger' : 'success'">{{numberOfNotifications}}</ion-badge>
    </ion-label>
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
  IonBadge,
  IonButton,
  IonIcon,
  IonLabel,
  IonModal,
} from '@ionic/vue';

import {
  notificationsOutline,
  notificationsOff,
  notifications
} from 'ionicons/icons';

import { useStore } from '@/store/store';
import { computed, onMounted, ref } from 'vue';
import Modal from './notification-modal.vue'

export default  {
  name: 'LogIn',
  components: {
    IonBadge,
    IonButton,
    IonIcon,
    IonLabel,
    IonModal,
    Modal,
  },
  setup() {
    const store = useStore();
    const isOpenRef = ref(false);
    const setOpen = (state: boolean) => isOpenRef.value = state;
    onMounted(async () => {
      await store.dispatch('loadNotifications');
    })

    const numberOfNotifications = computed(() => store.getters.notifications.length);
    return {
      setOpen,
      numberOfNotifications,
      isOpenRef,
      notificationsOutline,
      notifications,
      notificationsOff,
    }
  }
}
</script>

<style scoped>
.notification-button {
  --padding-bottom: 0px;
  --padding-start: 0px;
  --padding-top: 0px;
  --padding-end: 0px;
}
</style>