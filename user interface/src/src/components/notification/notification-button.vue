<template>
  <ion-button
    class="notification-button"
    fill="clear"
    @click='handleClick'
  >
    <ion-icon :icon="notificationsOutline" />
    <ion-label position="stacked">
      <ion-badge color="danger">1</ion-badge>
    </ion-label>
  </ion-button>
</template>

<script lang="ts">
import {
  IonBadge,
  IonButton,
  IonIcon,
  IonLabel,
} from '@ionic/vue';

import {
  notificationsOutline,
  notificationsOff,
  notifications
} from 'ionicons/icons';


export default  {
  name: 'LogIn',
  components: {
    IonBadge,
    IonButton,
    IonIcon,
    IonLabel,
  },
  setup() {
    const handleClick = () => {
      console.log('I am clicked');
    }
    return {
      handleClick,
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