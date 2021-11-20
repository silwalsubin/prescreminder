<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Log In</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" class="log-in-container">
      <div>
        <ion-item>
          <ion-label><ion-icon :icon="mail" /></ion-label>
          <ion-input
            v-model="form.emailAddressOrUserName" 
            placeholder="Email Address or UserName*" 
            :clear-input="true"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="key" /></ion-label>
          <ion-input
            v-model="form.password" 
            placeholder="Password*" 
            type="password" 
            :clear-input="true"
          />
        </ion-item>
        <br>
        <div class="log-in-buttons">
          <ion-button 
            size="large" 
            shape="round" 
            color="success"
            :disabled="!canLogIn"
            @click="asyncLogin"
          >
          Log In
          </ion-button>
          <ion-router-link href="/">
            <ion-button 
              size="large" 
              shape="round" 
              color="light"
              :disabled="false"
            >
            Cancel
            </ion-button>
          </ion-router-link>
        </div>
      </div>
    </ion-content>
  </ion-page>
</template>

<script lang="ts">
import {
  IonButton,
  IonContent, 
  IonHeader,
  IonIcon,
  IonItem,
  IonInput,
  IonLabel,
  IonPage,
  IonTitle, 
  IonToolbar,
} from '@ionic/vue';

import {
  key,
  mail,
} from 'ionicons/icons';

import { ref, computed } from 'vue'
import { useStore } from '@/store/store'
import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';

export default  {
  name: 'LogIn',
  components: {
    IonButton,
    IonContent,
    IonHeader,
    IonIcon,
    IonItem,
    IonInput,
    IonLabel,
    IonPage,
    IonTitle, 
    IonToolbar, 
  },
  setup() {
    const store = useStore();
    const router = useRouter();
    
    const form = ref({
      emailAddressOrUserName: '',
      password: '',
    })

    const inProgress = ref(false);

    const asyncLogin = async () => {
      inProgress.value = true;
      try {
        await store.dispatch('logIn', form.value);
        router.push({
          name: RouteName.DashboardTabDefault
        })
      } catch (error) {
        console.log(error)
      } finally {
        inProgress.value = false;
      }
    }

    const canLogIn = computed(() => {
      return form.value.emailAddressOrUserName !== ''
              && form.value.password !== ''
              && !inProgress.value;
    })
    return {
      asyncLogin,
      canLogIn,
      form,
      key,
      mail,
    }
  }
}
</script>

<style scoped>
.log-in-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  text-align: center;
}

.log-in-buttons > *:not(:first-child) {
  margin-left: 10px;
}
</style>