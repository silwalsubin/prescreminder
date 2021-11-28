<template>
  <ion-item lines="full" class="medication-info-ion-item">
    <div class="medication-info-item">
      <ion-label
        :color="isTaken ? 'medium': 'dark'" 
        position="stacked"
      >
        {{medicationInfo.name}} {{medicationInfo.quantity}}
      </ion-label>
      <ion-text :color="isTaken ? 'medium': medicationInfoColor">
        <p>
        {{humanizedTime}}
        </p>
      </ion-text>
    </div>
    <ion-toggle slot="end"
      @ionChange="handleTakenChange"
      :checked="isTaken"
      color="success">
    </ion-toggle>
  </ion-item>
</template>

<script>
import { computed } from 'vue';
import moment from 'moment';
import { useStore } from '@/store/store'
import MedicationInfoViewModel from '@/store/view-models/medication-info-view-model';
import {
  IonItem,
  IonLabel,
  IonText,
  IonToggle,
} from '@ionic/vue';

export default {
  props: {
    medicationInfo: { type: MedicationInfoViewModel, required: true }
  },
  components: {
    IonItem,
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
      if (minutes <= -60) return "danger";
      if (minutes <= 0) return "warning";
      else return "primary";
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
</style>