<template>
  <form-item>
    <div class="medication-info-item">
      <ion-label
        :color="isTaken ? 'medium': 'dark'" 
      >
        {{medicationInfo.name}} {{medicationInfo.quantity}}
      </ion-label>
      <ion-text 
        :color="isTaken ? 'medium': medicationInfoColor"
      >
        <h1 class="medication-info--time">{{humanizedTime}}</h1>
      </ion-text>
    </div>
    <ion-toggle slot="end"
      @ionChange="handleTakenChange"
      :checked="!isTaken"
      color="success"
      class="medication-info--toggle"
    />
  </form-item>
</template>

<script>
import { computed } from 'vue';
import moment from 'moment';
import { useStore } from '@/store/store'
import FormItem from "@/components/form-elements/form-item.vue";
import MedicationInfoViewModel from '@/store/view-models/medication-info-view-model';
import {
  IonLabel,
  IonText,
  IonToggle,
} from '@ionic/vue';

export default {
  props: {
    medicationInfo: { type: MedicationInfoViewModel, required: true }
  },
  components: {
    FormItem,
    IonLabel,
    IonText,
    IonToggle,
  },
  setup(props) {
    const store = useStore();

    const medicationInfoDateTimeMoment = computed(() => {
      return moment().set('hour', props.medicationInfo.hour)
      .set('minute', props.medicationInfo.minute)
    })
    const humanizedTime = computed(() => {
      return medicationInfoDateTimeMoment.value.format("hh:mm A");
    })
    const medicationInfoColor = computed(() => {
      const minutes = medicationInfoDateTimeMoment.value.diff(moment(), 'minutes');
      if (minutes <= 0) return "danger";
      if (minutes <= 10) return "warning";
      else return "dark";
    })
    const handleTakenChange = () => {
      store.dispatch('updateMedicationTaken', props.medicationInfo);
    }
    const isTaken = computed(() => props.medicationInfo.historyId !== null)
    return {
      isTaken,
      handleTakenChange,
      humanizedTime,
      medicationInfoColor,
    }
  }
}
</script>

<style scoped>
.medication-info-ion-item {
  --border-radius: 5px;
  --inner-padding-top: 15px;
}

.medication-info--time {
  margin-top: 10px;
}

.medication-info--toggle {
  margin-top: -15px;
}
</style>