<template>
  <div>
    <ion-header>
      <ion-toolbar>
        <ion-title>{{ prescriptionId ? 'Edit Prescription' : 'Add Prescription' }}</ion-title>
        <ion-button 
          slot="end"
          fill="clear"
          @click="handleCancel"
          :disabled="formDisabled"
        >
          Cancel
        </ion-button>
      </ion-toolbar>
    </ion-header>
    <ion-content :fullscreen="true" slot="fixed">
      <input-field
        v-model:inputValue="form.name" 
        label="Name of medication"
        placeholder="Required"
        :disabled="formDisabled"
      />
      <input-field
        v-model:inputValue="form.unitDose"
        label="Unit Dose"
        placeholder="Required"
        :disabled="formDisabled"
      />
      <input-field
        v-model:inputValue="form.totalQuantity"
        label="Total Quantity"
        placeholder="Required"
        type="number"
        :disabled="formDisabled"
      />
      <date-picker
        v-model:inputValue="form.startDate"
        label="Start Date"
        placeholder="Required"
        :disabled="formDisabled"
        :max="String(new Date().getFullYear() + 10)"
        :min="String(new Date().toISOString().split('T')[0])"
      />
      <date-picker
        v-model:inputValue="form.completeDate"
        label="End Date"
        placeholder="Optional"
        :disabled="formDisabled"
        :max="String(new Date().getFullYear() + 10)"
        :min="String(new Date().toISOString().split('T')[0])"
      />
      <div class="medication-intake-items">
        <ion-text color="medium">
          <h5>Medication intake times</h5>
        </ion-text>
        <ion-button
          color="success"
          shape="round"
          size="default"
          @click="handleAddTimeOfDay"
          :disabled="formDisabled"
        >
        New Time
        </ion-button>
      </div>

      <div class="time-of-day-container" v-for="(timeOfDay, index) in form.timesOfDay" :key="timeOfDay.id">
        <ion-button
          fill="outline"
          color="medium"
          class="time-of-day-button"
        > 
          <time-picker
            :hour="timeOfDay.hour"
            :minute="timeOfDay.minute"
            @input="handleTimeOfDayChange($event, index)"
            :disabled="formDisabled"
          />
        </ion-button>
        <ion-button
          v-if="form.timesOfDay.length > 1"
          shape="round"
          fill="outline"
          color="danger"
          size="small"
          :disabled="formDisabled"
          @click="handleDelete(index)"
        >
          Delete
        </ion-button>
      </div>
      <ion-button
        class="save-button"
        size="large" 
        color="success"
        expand="full"
        :disabled="!isFormValid || formDisabled"
        @click="handleConfirm"
      >
      Save
      </ion-button>
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
  IonText,
} from '@ionic/vue';
import { ref, computed, onMounted } from 'vue'
import TimePicker from './time-picker.vue';
import DatePicker from './form-elements/date-picker.vue';
import InputField from './form-elements/input-field.vue';
import { notifyAsync, NotificationType } from '@/toast-notifications'
import TimeOfDay from "@/store/domain/time-of-day";

import {
  informationCircle,
  timeOutline,
  trashOutline,
  addCircleOutline
} from 'ionicons/icons';

import { useStore } from '@/store/store'

export default {
  name: 'PrescriptionModal',
  components: {
    DatePicker,
    InputField,
    IonButton, 
    IonContent,
    IonHeader,
    IonTitle,
    IonText,
    IonToolbar,
    TimePicker,
  },
  props: {
    isVisible: { type: Boolean, required: true },
    prescriptionId: { type: String, required: false },
  },
  setup(props, {emit}) {
    const store = useStore();
    const formDisabled = ref(false);
    const form = ref(store.getters.addPrescriptionPayload);

    const handleAddTimeOfDay = () => {
      form.value.timesOfDay.push(new TimeOfDay(0, 0))
    }

    const handleTimeOfDayChange = (hourMinute, index) => {
      form.value.timesOfDay[index].hour = hourMinute.hour;
      form.value.timesOfDay[index].minute = hourMinute.minute;
    }

    const handleDelete = (index) => {
      form.value.timesOfDay.splice(index, 1);
    }

    const isFormValid = computed(() => {
      const formValue = form.value;
      return formValue.name.trim() !== ''
              && formValue.unitDose.trim() !== ''
              && formValue.startDate
              && formValue.timesOfDay.length > 0
              && Number(formValue.totalQuantity) >= 1
    })

    const handleConfirm = async () => {
      formDisabled.value = true;
      if (props.prescriptionId){
        await store.dispatch('updatePrescription', {
          prescriptionId: props.prescriptionId,
          viewModal: form.value
        });
      } else {
        await store.dispatch('addPrescription', form.value);
      }
      formDisabled.value = false;
      await notifyAsync(NotificationType.Success, "Saved successfully")
      emit('close');
    }

    onMounted(() => {
      if (props.prescriptionId){
        const prescription = store.getters.prescriptions.find(x => x.prescriptionId === props.prescriptionId);
        form.value = prescription;
      } else {
        form.value = store.getters.addPrescriptionPayload;
      }
    })

    const handleCancel = () => {
      emit('close');
    }

    return {
      addCircleOutline,
      formDisabled,
      form,
      isFormValid,
      informationCircle,
      timeOutline,
      trashOutline,
      handleAddTimeOfDay,
      handleConfirm,
      handleTimeOfDayChange,
      handleCancel,
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
  padding-left: 10px;
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

<style scoped lang="scss">
@import "@/styles/animations/blinking-animation.scss";
.save-button {
  animation: $blink-default;
}

.time-of-day-button {
  width: 200px;
}
</style>