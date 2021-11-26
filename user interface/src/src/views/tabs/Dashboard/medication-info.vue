<template>
  <ion-item lines="full"  class="medication-info-ion-item">
    <div class="medication-info-item">
      <ion-label color="dark" position="stacked">{{medicationInfo.name}} {{medicationInfo.quantity}}</ion-label>
      <ion-text :color="medicationInfoColor">
        <p>
        {{humanizedTime}}
        </p>
      </ion-text>
    </div>
    <ion-toggle slot="end"
      color="success">
    </ion-toggle>
  </ion-item>
</template>

<script>
import { computed } from 'vue';
import moment from 'moment';
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
    const medicationInfoDateTimeMoment = computed(() => {
      return moment().set('hour', props.medicationInfo.hour)
      .set('minute', props.medicationInfo.minute)
    })
    const humanizedTime = computed(() => {
      return medicationInfoDateTimeMoment.value.fromNow();
    })
    const medicationInfoColor = computed(() => {
      const minutes = medicationInfoDateTimeMoment.value.diff(moment(), 'minutes');
      if (minutes <= -60) return "danger";
      if (minutes <= 0) return "warning";
      if (minutes <= 60) return "primary";
      else return "secondary";
    })
    return {
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