<template>
  <form-item
    :label="label"
  >
    <ion-input
      v-model="vModel" 
      :placeholder="placeholder" 
      :clear-input="true"
      :type="type"
    />
 </form-item>
</template>

<script>
import {
  IonInput, 
} from '@ionic/vue';

import { computed } from 'vue'
import FormItem from '@/components/form-elements/form-item.vue';

export default {
  name: 'inputField',
  components: {
    FormItem,
    IonInput,
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