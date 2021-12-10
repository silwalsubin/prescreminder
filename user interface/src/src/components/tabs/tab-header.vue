<template>
  <ion-header>
    <ion-toolbar>
      <ion-title>{{headerTitle}}</ion-title>
      <notification-button slot="end" />
    </ion-toolbar>
  </ion-header>
</template>

<script lang="ts">
import { 
  IonHeader,
  IonToolbar,
  IonTitle,
} from '@ionic/vue';

import NotificationButton from '@/components/notification/notification-button.vue'

export default  {
  components: {
    IonHeader,
    IonToolbar, 
    IonTitle,
    NotificationButton,
  },
  props: {
    headerTitle : { type: String, required: true }
  }
}
</script>