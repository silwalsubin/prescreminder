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
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.userName" 
            placeholder="Username*" 
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
        <ion-item>
          <ion-label><ion-icon :icon="key" /></ion-label>
          <ion-input
            v-model="form.confirmedPassword" 
            placeholder="Confirm Password*"
            type="password"
            :clear-input="true"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.firstName"
            placeholder="First Name*"
            :clear-input="true"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.middleName" 
            placeholder="Middle Name"
            :clear-input="true"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="person" /></ion-label>
          <ion-input
            v-model="form.lastName" 
            placeholder="Last Name*"
            :clear-input="true"
          />
        </ion-item>
        <ion-item>
          <ion-label><ion-icon :icon="calendarNumber" /></ion-label>
          <ion-datetime
            v-model="form.dateOfBirth"
            display-format="MM/DD/YYYY"
            placeholder="Date of Birth*"
          />
        </ion-item>
        <br>
        <div class="register-buttons">
          <ion-router-link href="/">
            <ion-button 
              size="large" 
              shape="round" 
              color="success"
              :disabled="!canCreateAccount"
            >
            Create Account
            </ion-button>
          </ion-router-link>
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
    const form = ref({
      emailAddress: '',
      userName: '',
      password: '',
      confirmedPassword: '',
      firstName: '',
      middleName: '',
      lastName: '', 
      dateOfBirth: '',
    })

    const canCreateAccount = computed(() => {
      return form.value.emailAddress !== ''
              && form.value.userName !== ''
              && form.value.password !== ''
              && form.value.confirmedPassword !== ''
              && form.value.firstName !== ''
              && form.value.lastName !== ''
              && form.value.dateOfBirth !== '';
    })
    return {
      canCreateAccount,
      form,
      calendarNumber,
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