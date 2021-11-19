<template>
  <div>
    <ion-header>
      <ion-toolbar>
        <ion-title>{{ title }}</ion-title>
        <ion-button 
          slot="end"
          fill="clear"
          :disabled="!isFormValid"
          @click="handleConfirm"
        >
          Add
        </ion-button>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" :scrollY="true">
      <input-field
        v-model:inputValue="form.name"
        label="Name of medication"
        placeholder="Required"
      />
      <input-field
        v-model:inputValue="form.quantity"
        label="Quantity of each intake"
        placeholder="Required"
      />
      <date-picker
        v-model:inputValue="form.startDate"
        label="Date to begin the medication"
        placeholder="Required"
      />
      <date-picker
        v-model:inputValue="form.expirationDate"
        label="Date till the mediation will last"
        placeholder="Optional"
      />
      <date-picker
        v-model:inputValue="form.completeDate"
        label="Date the medication is no longer required"
        placeholder="Optional"
      />
      <div class="medication-intake-items">
        <h5>Medication intake times</h5>
        <ion-button
          color="success"
          shape="round"
          size="default"
          @click="handleAddTimeOfDay"
        >
        Add New
        </ion-button>
      </div>

      <div class="time-of-day-container" v-for="timeOfDay in form.timesOfDay" :key="timeOfDay.id">
        <a>
          <time-picker
            :hour="timeOfDay.hour"
            :minute="timeOfDay.minute"
            @input="handleTimeOfDayChange($event, timeOfDay.id)"
          />
        </a>
        <ion-button
          v-if="form.timesOfDay.length > 1"
          shape="round"
          fill="outline"
          color="danger"
          size="small"
          @click="handleDelete(timeOfDay.id)"
        >
          Delete
        </ion-button>
      </div>
    </ion-content>
  </div>
</template>

<script>
import { 
  IonButton, 
  IonContent,
  IonHeader,
  IonTitle, 
  IonToolbar,
} from '@ionic/vue';
import { ref, computed } from 'vue'
import TimePicker from './time-picker.vue';
import DatePicker from './form-elements/date-picker.vue';
import InputField from './form-elements/input-field.vue';
import { Guid } from 'guid-typescript';

import {
  informationCircle,
  timeOutline,
  trashOutline,
  addCircleOutline
} from 'ionicons/icons';
// import moment from 'moment';

import { useStore } from '../store/store'

export default {
  name: 'PrescriptionModal',
  components: {
    DatePicker,
    InputField,
    IonButton, 
    IonContent,
    IonHeader,
    IonTitle, 
    IonToolbar,
    TimePicker,
  },
  props: {
    title: { type: String, required: true },
  },
  setup() {
    const store = useStore();

    const form = ref(store.getters.addPrescriptionPayload);
    // const form = ref({
    //   name: '',
    //   quantity: '',
    //   startDate: moment().toISOString(),
    //   completeDate: null,
    //   expirationDate: null,
    //   timesOfDay: [
    //     { id: Guid.create(), hour: 11, minute: 22 },
    //   ]
    // });

    const handleAddTimeOfDay = () => {
      form.value.timesOfDay.push({ id: Guid.create(), hour: 0, minute: 0 })
    }

    const handleTimeOfDayChange = (hourMinute, id) => {
      const index = form.value.timesOfDay.findIndex(x => x.id === id);
      form.value.timesOfDay[index].hour = hourMinute.hour;
      form.value.timesOfDay[index].minute = hourMinute.minute;
    }

    const handleDelete = (id) => {
      const index = form.value.timesOfDay.findIndex(x => x.id === id);
      form.value.timesOfDay.splice(index, 1);
    }

    const isFormValid = computed(() => {
      const formValue = form.value;
      return formValue.name.trim() !== ''
              && formValue.quantity.trim() !== ''
              && formValue.startDate
              && formValue.timesOfDay.length > 0
    })

    const handleConfirm = () => {
      store.dispatch('addPrescription', form);
    }

    return {
      addCircleOutline,
      form,
      isFormValid,
      informationCircle,
      timeOutline,
      trashOutline,
      handleAddTimeOfDay,
      handleConfirm,
      handleTimeOfDayChange,
      handleDelete,
    }
  }
}
</script>

<style scoped>
.time-of-day-container {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding-right: 20px;
}

.medication-intake-items {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-top: 15px;
  padding-left: 15px;
  padding-right: 15px;
}
</style>