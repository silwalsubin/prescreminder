<template>
  <ion-page>
    <ion-header>
      <ion-toolbar>
        <ion-title>Create Account</ion-title>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" class="register-container">
      <div>
        <input-field
          v-model:inputValue="form.emailAddress"
          label="Email Address"
          placeholder="Required"
        />
        <input-field
          v-model:inputValue="form.userName"
          label="Username"
          placeholder="Required"
        />
        <input-field
          v-model:inputValue="form.password"
          label="Password"
          placeholder="Required"
          type="password" 
        />
        <input-field
          v-model:inputValue="form.confirmedPassword"
          label="Confirm Password"
          placeholder="Required"
          type="password" 
        />
        <input-field
          v-model:inputValue="form.firstName"
          label="First Name"
          placeholder="Required"
        />
        <input-field
          v-model:inputValue="form.middleName"
          label="Middle Name"
          placeholder="Optional"
        />
        <input-field
          v-model:inputValue="form.lastName"
          label="Last Name"
          placeholder="Required"
        />
        <date-picker
          v-model:inputValue="form.dateOfBirth"
          label="Date of Birth"
          placeholder="Required"
          :min="birthDateMin"
        />
        <br>
        <div class="register-buttons">
          <ion-button 
            size="large" 
            shape="round" 
            color="success"
            :disabled="!canCreateAccount"
          >
          Create Account
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
import { useRouter } from 'vue-router';
import { RouteName } from '@/router/route-names';
import InputField from '@/components/form-elements/input-field.vue'
import DatePicker from '@/components/form-elements/date-picker.vue'
import moment from 'moment';

export default  {
  name: 'Register',
  components: {
    DatePicker,
    InputField,
    IonButton,
    IonContent,
    IonHeader,
    IonPage,
    IonTitle, 
    IonToolbar,
  },
  setup() {
    const router = useRouter();

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

    const birthDateMin = computed(() => moment().local().format('YYYY-MM-DD'));

    const handleCancel = () => {
      router.push({
        name: RouteName.WelcomePage
      })
    }

    return {
      birthDateMin,
      canCreateAccount,
      form,
      handleCancel,
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