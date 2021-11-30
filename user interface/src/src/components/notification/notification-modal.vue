<template>
  <div>
    <ion-header>
      <ion-toolbar>
        <ion-title>Notifications</ion-title>
        <ion-button 
          slot="end"
          fill="clear"
          @click="handleCancel"
        >
          Close
        </ion-button>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" slot="fixed">
      <ion-item
        class="notification-item"
        v-for="item in notifications" 
        :key="item" 
        lines="full"
      >
        <ion-label 
          position="stacked" 
          :color="getItemColor(item.eventDate)"
        >
          {{ humanizedTime(item.eventDate) }}
        </ion-label>
        {{item.event}}
        <ion-button 
          slot="end"
          fill="clear"
          @click="handleClear(item.notificationId)"
        >
          Clear
        </ion-button>
      </ion-item>
    </ion-content>
  </div>
</template>

<script>
import { 
  IonButton, 
  IonContent,
  IonHeader,
  IonItem,
  IonLabel,
  IonTitle, 
  IonToolbar,
} from '@ionic/vue';
import { computed } from 'vue'
import { useStore } from '@/store/store'
import moment from 'moment'

export default {
  name: 'NotificationsModal',
  components: {
    IonButton, 
    IonContent,
    IonHeader,
    IonItem,
    IonLabel,
    IonTitle,
    IonToolbar,
  },
  setup(props, {emit}) {
    const store = useStore();
    const notifications = computed(() => store.getters.notifications) 
    const handleCancel = () => {
      emit('close');
    }

    const handleClear = async (notificationid) => {
      await store.dispatch('clearNotification', notificationid);
    };

    const getItemColor = ((eventDate) => {
      const days = moment(eventDate).diff(moment(), 'day');
      if (days < 7) return 'danger';
      else return 'warning';
    });

    const humanizedTime = ((eventDate) => {
      return moment(eventDate).fromNow();
    })

    return {
      getItemColor,
      humanizedTime,
      notifications,
      handleCancel,
      handleClear,
    }
  }
}
</script>

<style scoped>
.notification-item {
  --inner-padding-bottom: 15px;
}
</style>