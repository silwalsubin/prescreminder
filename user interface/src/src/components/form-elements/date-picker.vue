<template>
  <form-item
    :label="label"
  >
    <ion-datetime
      v-model="vModel"
      display-format="MM/DD/YYYY"
      :placeholder="placeholder"
      :min="min"
      :max="max"
    />
  </form-item>
</template>

<script>
import {
  IonDatetime, 
} from '@ionic/vue';

import moment from 'moment';
import { computed } from 'vue'
import FormItem from '@/components/form-elements/form-item.vue';

export default {
  name: 'datePicker',
  components: {
    FormItem,
    IonDatetime,
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