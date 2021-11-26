<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Log In</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" class="log-in-container">
      <div>
        <input-field
          v-model:inputValue="form.emailAddressOrUserName"
          label="Email Address or UserName"
          placeholder="Required"
        />
        <input-field
          v-model:inputValue="form.password"
          label="Password"
          placeholder="Required"
          type="password"
        />
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
          <ion-button 
            size="large" 
            shape="round" 
            color="light"
            :disabled="false"
            @click="handleCancel"
          >
          Cancel
          </ion-button>
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
  IonPage,
  IonTitle, 
  IonToolbar,
} from '@ionic/vue';

import { ref, computed } from 'vue'
import { useStore } from '@/store/store'
import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';
import InputField from '@/components/form-elements/input-field.vue'

export default  {
  name: 'LogIn',
  components: {
    InputField,
    IonButton,
    IonContent,
    IonHeader,
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
          name: RouteName.DashboardTab
        })
      } catch (error) {
        console.log(error)
      } finally {
        inProgress.value = false;
      }
    }

    const handleCancel = () => {
      router.push({
        name: RouteName.WelcomePage
      })
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
      handleCancel,
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