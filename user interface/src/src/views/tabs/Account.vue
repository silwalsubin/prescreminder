<template>
  <ion-page>
    <tab-header header-title="My Account"/>
    <ion-content :fullscreen="true">   
      <div class="account-tab-container">
        <ion-button 
          size="large" 
          shape="round" 
          color="primary"
          @click="asyncLogOut"
        >
        Sign Out
        </ion-button>
        <ion-button 
          size="large" 
          shape="round" 
          color="danger"
          @click="asyncDeleteAccount"
        >
        Delete Account
        </ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import {
  IonButton,
  IonPage,
  IonContent
} from '@ionic/vue';
import TabHeader from '@/components/tabs/tab-header.vue';
import { notifyAsync, NotificationType } from '@/toast-notifications';

import { useStore } from '@/store/store'
import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';

export default  {
  name: 'AccountTab',
  components: { 
    IonButton,
    IonContent, 
    IonPage,
    TabHeader,
  }, 
  setup() {
    const store = useStore();
    const router = useRouter();

    const asyncLogOut = async () => {
      await store.dispatch('logOut');
      await notifyAsync(NotificationType.Success, "Logged Out Successfully");
      router.push({
        name: RouteName.WelcomePage
      });
    }

    const asyncDeleteAccount = async () => {
      await store.dispatch('deleteAccount');
      await notifyAsync(NotificationType.Success, "Account Deleted Successfully");
      router.push({
        name: RouteName.WelcomePage
      });      
    }

    return {
      asyncLogOut,
      asyncDeleteAccount,
    }
  }
}
</script>

<style scoped>
.account-tab-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}

.account-tab-container > *:not(:first-child)  {
  margin-left: 10px;
}
</style>