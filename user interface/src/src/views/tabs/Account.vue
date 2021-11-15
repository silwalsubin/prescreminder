<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>My Account</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true">   
      <div class="account-tab-container">
          <ion-button 
            size="large" 
            shape="round" 
            color="primary"
            :disabled="false"
            @click="asyncLogOut"
          >
          Sign Out
          </ion-button>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import {
  IonButton,
  IonPage,
  IonHeader, 
  IonToolbar, 
  IonTitle, 
  IonContent
} from '@ionic/vue';

import { useStore } from '../../store/store'
import { useRouter } from 'vue-router';

export default  {
  name: 'AccountTab',
  components: { 
    IonButton,
    IonHeader, 
    IonToolbar, 
    IonTitle, 
    IonContent, 
    IonPage, 
  }, 
  setup() {
    const store = useStore();
    const router = useRouter();

    const asyncLogOut = async () => {
      await store.dispatch('logOut');
      router.push({
        name: 'WelcomePage'
      })
    }

    return {
      asyncLogOut
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
</style>