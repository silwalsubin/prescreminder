<template>
  <ion-item>
    <ion-label position="stacked" >
      {{ label }}
    </ion-label>
    <ion-input
      v-model="vModel" 
      :placeholder="placeholder" 
      :clear-input="true"
      :type="type"
    />
  </ion-item>
</template>

<script>
import {
  IonInput, 
  IonItem,
  IonLabel,
} from '@ionic/vue';

import { computed } from 'vue'

export default {
  name: 'inputField',
  components: {
    IonInput,
    IonItem,
    IonLabel,
  },
  props: {
    inputValue: { type: String },
    placeholder: { type: String, required: true },
    label: { type: String, required: true },
    type: { type: String, default: 'text' },
  },
  setup(props, {emit}) {
    const vModel = computed({ 
      get: () => props.inputValue, 
      set: (value) => emit('update:inputValue', value) 
    });

    return {
      vModel,
    }
  }
}
</script>