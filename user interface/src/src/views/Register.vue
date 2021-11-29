<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Create Account</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" class="register-container">
      <div>
        <ion-item>
          <ion-label><ion-icon :icon="mail" /></ion-label>
          <ion-input
            v-model="form.emailAddress" 
            placeholder="Email Address*" 
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.userName" 
            placeholder="Username*" 
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="key" /></ion-label>
          <ion-input
            v-model="form.password" 
            placeholder="Password*" 
            type="password" 
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="key" /></ion-label>
          <ion-input
            v-model="form.confirmPassword" 
            placeholder="Confirm Password*"
            type="password"
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.firstName"
            placeholder="First Name*"
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.middleName" 
            placeholder="Middle Name"
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.lastName" 
            placeholder="Last Name*"
            :clear-input="true"
            :disabled="inProgress"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="calendarNumber" /></ion-label>
          <ion-datetime
            v-model="form.dateOfBirth"
            display-format="MM/DD/YYYY"
            placeholder="Date of Birth*"
            :disabled="inProgress"
          />
        </ion-item>
        <br>
        <div class="register-buttons">
          <ion-button 
            size="large" 
            shape="round" 
            color="success"
            @click="asyncRegister"
            :disabled="!canCreateAccount || inProgress"
          >
          Create Account
          </ion-button>
          <ion-router-link href="/">
            <ion-button 
              size="large" 
              shape="round" 
              color="light"
              :disabled="inProgress"
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
  IonDatetime,
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
  calendarNumber,
  key,
  mail,
  person,
} from 'ionicons/icons';

import { ref, computed } from 'vue'
import { useStore } from '@/store/store'
import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';
import { notifyAsync, NotificationType } from '@/toast-notifications'

export default  {
  name: 'Register',
  components: {
    IonButton,
    IonContent,
    IonDatetime,
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
    const inProgress = ref(false);
    const form = ref({
      emailAddress: '',
      userName: '',
      password: '',
      confirmPassword: '',
      firstName: '',
      middleName: null,
      lastName: '', 
      dateOfBirth: '',
    })

    const canCreateAccount = computed(() => {
      return form.value.emailAddress !== ''
              && form.value.userName !== ''
              && form.value.password !== ''
              && form.value.confirmPassword !== ''
              && form.value.firstName !== ''
              && form.value.lastName !== ''
              && form.value.dateOfBirth !== '';
    })

    const asyncRegister = async () => {
      inProgress.value = true;
      try {
        await store.dispatch('register', form.value);
        await notifyAsync(NotificationType.Success, "Account Created Successfully");
        router.push({
          name: RouteName.LogInPage
        });
      } catch (error) {
        console.log(error);
      } finally {
        inProgress.value = false;
      }
    }

    return {
      asyncRegister,
      canCreateAccount,
      form,
      calendarNumber,
      inProgress,
      key,
      mail,
      person,
    }
  }
}
</script>

<style scoped>
.register-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  text-align: center;
}

.register-buttons > *:not(:first-child) {
  margin-left: 10px;
}
</style>