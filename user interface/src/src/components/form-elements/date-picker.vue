<template>
  <ion-item>
    <ion-label position="stacked">
      {{ label }}
    </ion-label>
    <ion-datetime
      v-model="vModel"
      display-format="MM/DD/YYYY"
      :placeholder="placeholder"
      :min="min"
      :max="max"
    />
  </ion-item>
</template>

<script>
import {
  IonDatetime, 
  IonItem,
  IonLabel,
} from '@ionic/vue';

import moment from 'moment';
import { computed } from 'vue'

export default {
  name: 'datePicker',
  components: {
    IonDatetime,
    IonItem,
    IonLabel,
  },
  props: {
    inputValue: { type: String },
    placeholder: { type: String, required: true },
    label: {type: String, required: true },
    max: {type: String, required: false },
    min: {type: String, required: false },
  },
  setup(props, {emit}) {
    const vModel = computed({ 
      get: () => props.inputValue, 
      set: (value) => {
        const time = moment(value);
        emit('update:inputValue', time.toISOString());
      }
    });

    return {
      vModel,
    }
  }
}
</script>